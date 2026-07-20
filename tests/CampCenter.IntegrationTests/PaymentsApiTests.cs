using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.DTOs.Sessions;
using CampCenter.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CampCenter.IntegrationTests;

/// Records register/verify calls and lets tests craft valid "notifications".
public class FakePaymentGateway : IPaymentGateway
{
    public List<GatewayRegisterRequest> Registered { get; } = [];

    public List<(string SessionId, long Amount, long OrderId)> Verified { get; } = [];

    public bool RejectSignature { get; set; }

    public Task<GatewayRegisterResult> RegisterTransactionAsync(
        GatewayRegisterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        Registered.Add(request);
        return Task.FromResult(
            new GatewayRegisterResult(
                "fake-token",
                $"https://sandbox.przelewy24.pl/trnRequest/fake-token-{request.SessionId}"
            )
        );
    }

    public bool VerifyNotificationSignature(GatewayNotification notification) =>
        !RejectSignature;

    public Task VerifyTransactionAsync(
        string sessionId,
        long amountGrosze,
        long orderId,
        CancellationToken cancellationToken = default
    )
    {
        Verified.Add((sessionId, amountGrosze, orderId));
        return Task.CompletedTask;
    }
}

public class PaymentsApiTests : IntegrationTestBase
{
    private static int _sessionOffset;

    private readonly FakePaymentGateway _gateway = new();
    private readonly HttpClient _client;
    private readonly HttpClient _admin;

    public PaymentsApiTests(CampCenterApiFactory factory)
        : base(factory)
    {
        var host = factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IPaymentGateway>();
                services.AddSingleton<IPaymentGateway>(_gateway);
            })
        );
        _client = host.CreateClient();
        _admin = host.CreateClient();
    }

    private static GatewayNotification Notification(
        GatewayRegisterRequest registered,
        long orderId = 555,
        long? amountOverride = null
    ) =>
        new(
            0,
            0,
            registered.SessionId,
            amountOverride ?? registered.AmountGrosze,
            registered.AmountGrosze,
            "PLN",
            orderId,
            154,
            "statement",
            "sign-checked-by-fake"
        );

    private async Task<(string Token, GatewayRegisterRequest Registered)> CreateBookingWithDepositAsync()
    {
        // Authenticate the admin client of the overridden host.
        var login = await _admin.PostAsJsonAsync(
            "/api/auth/login",
            new CampCenter.Application.DTOs.Auth.LoginRequestDto(AdminLogin, AdminPassword)
        );
        login.EnsureSuccessStatusCode();
        var loginBody =
            await login.Content.ReadFromJsonAsync<CampCenter.Application.DTOs.Auth.LoginResponseDto>();
        _admin.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginBody!.Token);

        // Capacity 9 rooms are unique to this test class; distinct session windows
        // avoid the published-overlap guard across tests.
        var offset = Interlocked.Increment(ref _sessionOffset) * 20;
        var suffix = Guid.NewGuid().ToString("N")[..6];
        await _admin.PostAsJsonAsync(
            "/api/admin/rooms",
            new CreateRoomRequestDto($"PAY-{suffix}", 9, null)
        );
        var sessionResponse = await _admin.PostAsJsonAsync(
            "/api/admin/sessions",
            new CreateCampSessionRequestDto(
                $"Turnus PAY {suffix}",
                new DateOnly(2033, 3, 1).AddDays(offset),
                new DateOnly(2033, 3, 10).AddDays(offset),
                100_000,
                25_000
            )
        );
        var session = (await sessionResponse.Content.ReadFromJsonAsync<CampSessionDto>())!;
        await _admin.PostAsync($"/api/admin/sessions/{session.Id}/publish", null);

        var create = await _client.PostAsJsonAsync(
            "/api/public/bookings",
            new CreateBookingRequestDto(
                session.Id,
                9,
                new Dictionary<int, int> { [9] = 1 },
                "Pay Org",
                "Jan Płatnik",
                "pay@example.com",
                "+48 600 000 111",
                null,
                "pl"
            )
        );
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        var booking = (await create.Content.ReadFromJsonAsync<CreateBookingResponseDto>())!;

        var initiate = await _client.PostAsJsonAsync(
            $"/api/public/bookings/{booking.ManageToken}/payments",
            new InitiatePaymentRequestDto("Deposit")
        );
        Assert.Equal(HttpStatusCode.OK, initiate.StatusCode);
        var redirect = (await initiate.Content.ReadFromJsonAsync<InitiatePaymentResponseDto>())!;
        Assert.Contains("trnRequest", redirect.RedirectUrl);

        return (booking.ManageToken, _gateway.Registered[^1]);
    }

    [Fact]
    public async Task DepositWebhook_ConfirmsBooking_AndIsIdempotent()
    {
        var (token, registered) = await CreateBookingWithDepositAsync();
        Assert.Equal(9 * 25_000, registered.AmountGrosze);

        // Webhook completes the payment via transaction/verify → booking Confirmed.
        var webhook = await _client.PostAsJsonAsync(
            "/api/public/payments/p24/status",
            Notification(registered)
        );
        Assert.Equal(HttpStatusCode.OK, webhook.StatusCode);
        Assert.Single(_gateway.Verified, v => v.SessionId == registered.SessionId);

        var details = (
            await _client.GetFromJsonAsync<BookingDetailsDto>($"/api/public/bookings/{token}")
        )!;
        Assert.Equal("Confirmed", details.Status);
        Assert.Single(details.Payments, p => p.Kind == "Deposit" && p.Status == "Completed");

        // Retried notification is acknowledged without a second verify call.
        var retry = await _client.PostAsJsonAsync(
            "/api/public/payments/p24/status",
            Notification(registered)
        );
        Assert.Equal(HttpStatusCode.OK, retry.StatusCode);
        Assert.Single(_gateway.Verified, v => v.SessionId == registered.SessionId);

        // A second deposit cannot be initiated.
        var again = await _client.PostAsJsonAsync(
            $"/api/public/bookings/{token}/payments",
            new InitiatePaymentRequestDto("Deposit")
        );
        Assert.Equal(HttpStatusCode.BadRequest, again.StatusCode);

        // Final payment: initiate + webhook → fully paid.
        var final = await _client.PostAsJsonAsync(
            $"/api/public/bookings/{token}/payments",
            new InitiatePaymentRequestDto("Final")
        );
        Assert.Equal(HttpStatusCode.OK, final.StatusCode);
        var finalRegistered = _gateway.Registered[^1];
        Assert.Equal(9 * 75_000, finalRegistered.AmountGrosze);
        var finalWebhook = await _client.PostAsJsonAsync(
            "/api/public/payments/p24/status",
            Notification(finalRegistered, orderId: 556)
        );
        Assert.Equal(HttpStatusCode.OK, finalWebhook.StatusCode);
        details = (
            await _client.GetFromJsonAsync<BookingDetailsDto>($"/api/public/bookings/{token}")
        )!;
        Assert.Contains(details.Payments, p => p.Kind == "Final" && p.Status == "Completed");
    }

    [Fact]
    public async Task Webhook_AmountMismatch_IsRejected()
    {
        var (_, registered) = await CreateBookingWithDepositAsync();

        var webhook = await _client.PostAsJsonAsync(
            "/api/public/payments/p24/status",
            Notification(registered, amountOverride: 1)
        );

        Assert.Equal(HttpStatusCode.BadRequest, webhook.StatusCode);
        Assert.DoesNotContain(_gateway.Verified, v => v.SessionId == registered.SessionId);
    }

    [Fact]
    public async Task Webhook_BadSignature_IsRejected()
    {
        var (_, registered) = await CreateBookingWithDepositAsync();
        _gateway.RejectSignature = true;
        try
        {
            var webhook = await _client.PostAsJsonAsync(
                "/api/public/payments/p24/status",
                Notification(registered)
            );
            Assert.Equal(HttpStatusCode.BadRequest, webhook.StatusCode);
        }
        finally
        {
            _gateway.RejectSignature = false;
        }
    }
}

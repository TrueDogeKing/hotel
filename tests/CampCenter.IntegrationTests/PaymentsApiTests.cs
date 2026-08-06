using System.Net;
using System.Net.Http.Json;
using CampCenter.Application.DTOs.Public;
using CampCenter.Application.DTOs.Rooms;
using CampCenter.Application.DTOs.Schedule;
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

    public bool VerifyNotificationSignature(GatewayNotification notification) => !RejectSignature;

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

/// The online-payment flow, skipped while Przelewy24 is switched off. Payment is
/// recorded by the owner now (Booking.PaymentState, AdminPricingApiTests); these
/// stay so the gateway can be re-enabled without rewriting its cover.
public class PaymentsApiTests : IntegrationTestBase
{
    // Default per-night pricing from appsettings.json (grosze per person per night).
    private const long PricePerNight = 12_000;
    private const long DepositPerNight = 3_000;
    private const int Headcount = 9;
    private const int Nights = 10;

    private static int _windowOffset;

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

    private async Task<(
        string Token,
        GatewayRegisterRequest Registered
    )> CreateBookingWithDepositAsync()
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

        // A fresh capacity-9 room and a distinct date window per booking keep the
        // shared inventory free of cross-test interference.
        var offset = Interlocked.Increment(ref _windowOffset) * 30;
        var suffix = Guid.NewGuid().ToString("N")[..6];
        await _admin.PostAsJsonAsync(
            "/api/admin/rooms",
            new CreateRoomRequestDto($"PAY-{suffix}", 9, null)
        );

        var start = new DateOnly(2033, 3, 1).AddDays(offset);
        var end = start.AddDays(Nights);

        var create = await _client.PostAsJsonAsync(
            "/api/public/bookings",
            new CreateBookingRequestDto(
                start,
                end,
                Headcount,
                0,
                new Dictionary<int, int> { [9] = 1 },
                [],
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

    [Fact(
        Skip = "Przelewy24 is switched off: the endpoints these drive are commented out in PublicBookingsController / PublicPaymentsController. Re-enable with them."
    )]
    public async Task DepositWebhook_ConfirmsBooking_AndIsIdempotent()
    {
        var (token, registered) = await CreateBookingWithDepositAsync();
        Assert.Equal(Headcount * Nights * DepositPerNight, registered.AmountGrosze);

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
        Assert.Equal(
            Headcount * Nights * (PricePerNight - DepositPerNight),
            finalRegistered.AmountGrosze
        );
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

    /// Confirming the deposit must also lay down the group's meals. The hook lives
    /// in PaymentService.HandleNotificationAsync, so it belongs with the payment
    /// tests — they already own the only overridden host, and adding another one
    /// puts a second background sweeper on the shared database.
    [Fact(
        Skip = "Przelewy24 is switched off: the endpoints these drive are commented out in PublicBookingsController / PublicPaymentsController. Re-enable with them."
    )]
    public async Task DepositWebhook_GeneratesTheGroupsMeals()
    {
        var (token, registered) = await CreateBookingWithDepositAsync();

        var webhook = await _client.PostAsJsonAsync(
            "/api/public/payments/p24/status",
            Notification(registered, orderId: 901)
        );
        Assert.Equal(HttpStatusCode.OK, webhook.StatusCode);

        var schedule = (
            await _client.GetFromJsonAsync<PublicScheduleDto>(
                $"/api/public/bookings/{token}/schedule"
            )
        )!;

        Assert.Equal("Confirmed", schedule.Status);
        // Every day of the stay, departure day included.
        Assert.Equal(Nights + 1, schedule.Days.Count);
        // Arrival day: only dinner starts after the 15:00 cutoff.
        Assert.Equal("Dinner", Assert.Single(schedule.Days[0].Entries).MealKind);
        // Departure day: only breakfast ends before the 11:00 cutoff.
        Assert.Equal("Breakfast", Assert.Single(schedule.Days[^1].Entries).MealKind);
        Assert.All(schedule.Days[1..^1], day => Assert.Equal(3, day.Entries.Count));
    }

    [Fact(
        Skip = "Przelewy24 is switched off: the endpoints these drive are commented out in PublicBookingsController / PublicPaymentsController. Re-enable with them."
    )]
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

    [Fact(
        Skip = "Przelewy24 is switched off: the endpoints these drive are commented out in PublicBookingsController / PublicPaymentsController. Re-enable with them."
    )]
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

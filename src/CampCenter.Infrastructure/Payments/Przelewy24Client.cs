using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CampCenter.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace CampCenter.Infrastructure.Payments;

/// Przelewy24 REST client (transaction/register + transaction/verify).
public class Przelewy24Client : IPaymentGateway
{
    private const string Currency = "PLN";

    private readonly HttpClient _http;
    private readonly P24Settings _settings;

    public Przelewy24Client(HttpClient http, IOptions<P24Settings> settings)
    {
        _settings = settings.Value;
        _http = http;
        _http.BaseAddress = new Uri(_settings.BaseUrl.TrimEnd('/') + "/");
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_settings.PosId}:{_settings.ApiKey}"))
        );
    }

    public async Task<GatewayRegisterResult> RegisterTransactionAsync(
        GatewayRegisterRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var payload = new
        {
            merchantId = _settings.MerchantId,
            posId = _settings.PosId,
            sessionId = request.SessionId,
            amount = request.AmountGrosze,
            currency = Currency,
            description = request.Description,
            email = request.Email,
            country = "PL",
            language = request.Language,
            urlReturn = request.UrlReturn,
            urlStatus = $"{_settings.ApiBaseUrl.TrimEnd('/')}/api/public/payments/p24/status",
            sign = P24SignCalculator.RegisterSign(
                request.SessionId,
                _settings.MerchantId,
                request.AmountGrosze,
                Currency,
                _settings.CrcKey
            ),
        };

        var response = await _http.PostAsJsonAsync(
            "api/v1/transaction/register",
            payload,
            cancellationToken
        );
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new InvalidOperationException(
                $"P24 transaction/register failed ({(int)response.StatusCode}): {body}"
            );
        }

        var result = await response.Content.ReadFromJsonAsync<RegisterResponse>(cancellationToken);
        var token =
            result?.Data?.Token
            ?? throw new InvalidOperationException("P24 register response had no token.");
        return new GatewayRegisterResult(
            token,
            $"{_settings.BaseUrl.TrimEnd('/')}/trnRequest/{token}"
        );
    }

    public bool VerifyNotificationSignature(GatewayNotification notification) =>
        string.Equals(
            P24SignCalculator.NotificationSign(notification, _settings.CrcKey),
            notification.Sign,
            StringComparison.OrdinalIgnoreCase
        )
        && notification.MerchantId == _settings.MerchantId
        && notification.PosId == _settings.PosId;

    public async Task VerifyTransactionAsync(
        string sessionId,
        long amountGrosze,
        long orderId,
        CancellationToken cancellationToken = default
    )
    {
        var payload = new
        {
            merchantId = _settings.MerchantId,
            posId = _settings.PosId,
            sessionId,
            amount = amountGrosze,
            currency = Currency,
            orderId,
            sign = P24SignCalculator.VerifySign(
                sessionId,
                orderId,
                amountGrosze,
                Currency,
                _settings.CrcKey
            ),
        };

        var response = await _http.PutAsJsonAsync(
            "api/v1/transaction/verify",
            payload,
            cancellationToken
        );
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new InvalidOperationException(
                $"P24 transaction/verify failed ({(int)response.StatusCode}): {body}"
            );
        }
    }

    private sealed class RegisterResponse
    {
        [JsonPropertyName("data")]
        public RegisterData? Data { get; set; }
    }

    private sealed class RegisterData
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}

namespace CampCenter.Application.Interfaces;

/// <param name="SessionId">Our transaction id (the Payment Id as a string).</param>
/// <param name="AmountGrosze">Amount in grosze, computed server-side.</param>
/// <param name="Language">"pl" or "en" — the payment page language.</param>
public record GatewayRegisterRequest(
    string SessionId,
    long AmountGrosze,
    string Description,
    string Email,
    string Language,
    string UrlReturn
);

public record GatewayRegisterResult(string Token, string RedirectUrl);

/// The P24 status notification payload (webhook body).
public record GatewayNotification(
    long MerchantId,
    long PosId,
    string SessionId,
    long Amount,
    long OriginAmount,
    string Currency,
    long OrderId,
    long MethodId,
    string Statement,
    string Sign
);

/// Przelewy24 behind an interface so tests can fake it and the application layer
/// stays provider-agnostic.
public interface IPaymentGateway
{
    /// transaction/register → token + redirect URL for the hosted payment page.
    Task<GatewayRegisterResult> RegisterTransactionAsync(
        GatewayRegisterRequest request,
        CancellationToken cancellationToken = default
    );

    /// Validates the SHA-384 signature of a status notification.
    bool VerifyNotificationSignature(GatewayNotification notification);

    /// transaction/verify — final server-to-server confirmation. Throws on rejection.
    Task VerifyTransactionAsync(
        string sessionId,
        long amountGrosze,
        long orderId,
        CancellationToken cancellationToken = default
    );
}

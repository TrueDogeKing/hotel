using CampCenter.Application.DTOs.Public;

namespace CampCenter.Application.Interfaces;

public record InitiatePaymentRequestDto(string Kind);

public record InitiatePaymentResponseDto(string RedirectUrl);

public interface IPaymentService
{
    /// Creates a Pending payment for the booking (amount computed server-side)
    /// and registers a P24 transaction. Returns the hosted-payment redirect URL.
    Task<InitiatePaymentResponseDto> InitiateAsync(
        string manageToken,
        InitiatePaymentRequestDto request,
        CancellationToken cancellationToken = default
    );

    /// Handles the P24 status webhook: verifies the signature and the amount,
    /// calls transaction/verify, marks the payment Completed and advances the
    /// booking. Idempotent — an already-completed payment is acknowledged.
    Task HandleNotificationAsync(
        GatewayNotification notification,
        CancellationToken cancellationToken = default
    );
}

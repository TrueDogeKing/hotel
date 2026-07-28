using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CampCenter.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IBookingRepository _bookings;
    private readonly IPaymentGateway _gateway;
    private readonly ITokenService _tokenService;
    private readonly IEmailSender _email;
    private readonly IScheduleService _schedule;
    private readonly BookingSettings _settings;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(
        IBookingRepository bookings,
        IPaymentGateway gateway,
        ITokenService tokenService,
        IEmailSender email,
        IScheduleService schedule,
        IOptions<BookingSettings> settings,
        ILogger<PaymentService> logger
    )
    {
        _bookings = bookings;
        _gateway = gateway;
        _tokenService = tokenService;
        _email = email;
        _schedule = schedule;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<InitiatePaymentResponseDto> InitiateAsync(
        string manageToken,
        InitiatePaymentRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        var booking =
            await _bookings.GetByTokenHashAsync(
                _tokenService.HashRefreshToken(manageToken),
                cancellationToken
            ) ?? throw new NotFoundException("Booking not found.");

        var kind = request.Kind switch
        {
            "Deposit" => PaymentKind.Deposit,
            "Final" => PaymentKind.Final,
            _ => throw new BusinessRuleViolationException("Unknown payment kind."),
        };

        var completedKinds = (await _bookings.GetPaymentsAsync(booking.Id, cancellationToken))
            .Where(p => p.Status == PaymentStatus.Completed)
            .Select(p => p.Kind)
            .ToHashSet();

        // The amount is always derived from the booking snapshot — client input
        // never carries money.
        long amount;
        if (kind == PaymentKind.Deposit)
        {
            if (booking.Status != BookingStatus.PendingDeposit)
            {
                throw new BusinessRuleViolationException(
                    "The deposit can only be paid while the booking awaits it."
                );
            }

            if (completedKinds.Contains(PaymentKind.Deposit))
            {
                throw new ConflictException("The deposit is already paid.");
            }

            amount = booking.DepositGrosze;
        }
        else
        {
            if (booking.Status != BookingStatus.Confirmed)
            {
                throw new BusinessRuleViolationException(
                    "The final payment is available once the booking is confirmed."
                );
            }

            if (completedKinds.Contains(PaymentKind.Final))
            {
                throw new ConflictException("The final amount is already paid.");
            }

            amount = booking.TotalGrosze - booking.DepositGrosze;
        }

        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            BookingId = booking.Id,
            Kind = kind,
            AmountGrosze = amount,
            P24SessionId = "", // set below — the Payment Id is the P24 sessionId
            CreatedAt = DateTime.UtcNow,
        };
        payment.P24SessionId = payment.Id.ToString("N");

        await _bookings.AddPaymentAsync(payment, cancellationToken);
        await _bookings.SaveChangesAsync(cancellationToken);

        var stay = $"{booking.StartDate:dd.MM.yyyy}–{booking.EndDate:dd.MM.yyyy}";
        var description =
            kind == PaymentKind.Deposit
                ? $"Zaliczka — {stay} — {booking.OrganizationName}"
                : $"Dopłata — {stay} — {booking.OrganizationName}";
        var result = await _gateway.RegisterTransactionAsync(
            new GatewayRegisterRequest(
                payment.P24SessionId,
                amount,
                description,
                booking.Email,
                booking.Language,
                $"{_settings.PublicBaseUrl.TrimEnd('/')}/platnosc/powrot?token={manageToken}"
            ),
            cancellationToken
        );

        payment.P24Token = result.Token;
        await _bookings.SaveChangesAsync(cancellationToken);

        return new InitiatePaymentResponseDto(result.RedirectUrl);
    }

    public async Task HandleNotificationAsync(
        GatewayNotification notification,
        CancellationToken cancellationToken = default
    )
    {
        if (!_gateway.VerifyNotificationSignature(notification))
        {
            throw new BusinessRuleViolationException("Invalid notification signature.");
        }

        var payment =
            await _bookings.GetPaymentByP24SessionIdAsync(notification.SessionId, cancellationToken)
            ?? throw new NotFoundException("Unknown payment session.");

        // Idempotency: P24 retries notifications; a completed payment is simply acknowledged.
        if (payment.Status == PaymentStatus.Completed)
        {
            return;
        }

        if (notification.Amount != payment.AmountGrosze)
        {
            _logger.LogError(
                "P24 notification amount mismatch for payment {PaymentId}: expected {Expected}, got {Actual}.",
                payment.Id,
                payment.AmountGrosze,
                notification.Amount
            );
            throw new BusinessRuleViolationException("Notification amount mismatch.");
        }

        // Server-to-server confirmation before any money is considered received.
        await _gateway.VerifyTransactionAsync(
            payment.P24SessionId,
            payment.AmountGrosze,
            notification.OrderId,
            cancellationToken
        );

        payment.Status = PaymentStatus.Completed;
        payment.P24OrderId = notification.OrderId;
        payment.CompletedAt = DateTime.UtcNow;

        var booking = payment.Booking!;
        var confirmedNow =
            payment.Kind == PaymentKind.Deposit && booking.Status == BookingStatus.PendingDeposit;
        if (confirmedNow)
        {
            booking.Status = BookingStatus.Confirmed;
            booking.HoldExpiresAt = null;
        }

        await _bookings.SaveChangesAsync(cancellationToken);

        if (confirmedNow)
        {
            // Generation must never fail the webhook: P24 would retry, the retry
            // short-circuits on the already-Completed payment above, and generation
            // would then never run at all. Log instead — the admin's "generate
            // missing meals" action is the recovery path.
            try
            {
                await _schedule.GenerateMealsForBookingAsync(booking.Id, cancellationToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(
                    ex,
                    "Meal generation failed for confirmed booking {BookingId}.",
                    booking.Id
                );
            }

            await SendSafelyAsync(
                EmailTemplates.BookingConfirmed(booking),
                cancellationToken
            );
        }
        else if (payment.Kind == PaymentKind.Deposit && booking.Status == BookingStatus.Cancelled)
        {
            // Race lost: the sweeper released the rooms before the deposit landed.
            // Money must not vanish silently — this needs a human decision (restore
            // the booking if rooms are still free, or refund).
            _logger.LogError(
                "Deposit received for already-cancelled booking {BookingId} ({Email}, {AmountGrosze} gr). Manual resolution required.",
                booking.Id,
                booking.Email,
                payment.AmountGrosze
            );
            if (!string.IsNullOrEmpty(_settings.AdminAlertEmail))
            {
                await SendSafelyAsync(
                    new EmailMessage(
                        _settings.AdminAlertEmail,
                        "[CampCenter] Zaliczka dla anulowanej rezerwacji — wymagana decyzja",
                        $"Rezerwacja {booking.Id} ({booking.OrganizationName}, {booking.Email}) "
                            + $"została anulowana przed wpłatą, ale zaliczka {payment.AmountGrosze / 100m:N2} zł "
                            + "właśnie wpłynęła. Przywróć rezerwację albo zwróć wpłatę."
                    ),
                    cancellationToken
                );
            }
        }
    }

    private async Task SendSafelyAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        try
        {
            await _email.SendAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send payment email to {Email}.", message.To);
        }
    }
}

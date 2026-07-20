using CampCenter.Application.Interfaces;
using CampCenter.Application.Services;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;

namespace CampCenter.Api.Background;

/// Periodic booking upkeep: releases rooms of expired unpaid holds and marks
/// finished sessions' bookings as Completed. A 2-hour grace window protects
/// bookings with an in-flight payment attempt from being swept mid-checkout.
public class BookingMaintenanceService : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan PaymentGrace = TimeSpan.FromHours(2);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BookingMaintenanceService> _logger;

    public BookingMaintenanceService(
        IServiceScopeFactory scopeFactory,
        ILogger<BookingMaintenanceService> logger
    )
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(Interval);
        do
        {
            try
            {
                await SweepAsync(stoppingToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "Booking maintenance sweep failed.");
            }
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task SweepAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var bookings = scope.ServiceProvider.GetRequiredService<IBookingRepository>();
        var email = scope.ServiceProvider.GetRequiredService<IEmailSender>();

        var now = DateTime.UtcNow;

        var expired = await bookings.GetExpiredPendingAsync(
            now,
            now - PaymentGrace,
            cancellationToken
        );
        foreach (var booking in expired)
        {
            booking.Status = BookingStatus.Cancelled;
            booking.CancelReason = BookingCancelReason.DepositNotPaid;
            booking.HoldExpiresAt = null;
            bookings.RemoveAssignments(booking);
            _logger.LogInformation(
                "Cancelled booking {BookingId} — deposit not paid before hold expiry.",
                booking.Id
            );
        }

        var ended = await bookings.GetConfirmedEndedAsync(
            DateOnly.FromDateTime(now),
            cancellationToken
        );
        foreach (var booking in ended)
        {
            booking.Status = BookingStatus.Completed;
        }

        if (expired.Count > 0 || ended.Count > 0)
        {
            await bookings.SaveChangesAsync(cancellationToken);
        }

        // Cancellation emails only after the state is durable.
        foreach (var booking in expired.Where(b => b.CampSession is not null))
        {
            try
            {
                await email.SendAsync(
                    EmailTemplates.BookingCancelled(booking, booking.CampSession!),
                    cancellationToken
                );
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(
                    ex,
                    "Failed to send hold-expiry cancellation email for booking {BookingId}.",
                    booking.Id
                );
            }
        }
    }
}

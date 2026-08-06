using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using CampCenter.Application.Models;
using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace CampCenter.Application.Services;

public class PricingService : IPricingService
{
    /// A rate above this is a typo (10 000 zł per person per night), not a price.
    private const long MaxRateGrosze = 1_000_000;

    private readonly IPricingDefaultsRepository _defaults;
    private readonly BookingSettings _settings;

    public PricingService(IPricingDefaultsRepository defaults, IOptions<BookingSettings> settings)
    {
        _defaults = defaults;
        _settings = settings.Value;
    }

    public async Task<PricingDefaultsDto> GetAsync(CancellationToken cancellationToken = default)
    {
        var stored = await _defaults.GetAsync(cancellationToken);
        return stored is null
            ? new PricingDefaultsDto(
                _settings.PricePerPersonPerNightGrosze,
                _settings.SupervisorPricePerPersonPerNightGrosze,
                _settings.DepositPerPersonPerNightGrosze,
                null
            )
            : ToDto(stored);
    }

    public async Task<PricingDefaultsDto> UpdateAsync(
        UpdatePricingDefaultsRequestDto request,
        CancellationToken cancellationToken = default
    )
    {
        Guard(
            request.PricePerPersonPerNightGrosze,
            request.SupervisorPricePerPersonPerNightGrosze,
            request.DepositPerPersonPerNightGrosze
        );

        var stored = await _defaults.GetAsync(cancellationToken);
        if (stored is null)
        {
            stored = new PricingDefaults { Id = PricingDefaults.SingletonId };
            await _defaults.AddAsync(stored, cancellationToken);
        }

        stored.PricePerPersonPerNightGrosze = request.PricePerPersonPerNightGrosze;
        stored.SupervisorPricePerPersonPerNightGrosze =
            request.SupervisorPricePerPersonPerNightGrosze;
        stored.DepositPerPersonPerNightGrosze = request.DepositPerPersonPerNightGrosze;
        stored.UpdatedAt = DateTime.UtcNow;
        await _defaults.SaveChangesAsync(cancellationToken);
        return ToDto(stored);
    }

    private static void Guard(long price, long supervisorPrice, long deposit)
    {
        if (
            price is < 0 or > MaxRateGrosze
            || supervisorPrice is < 0 or > MaxRateGrosze
            || deposit is < 0 or > MaxRateGrosze
        )
        {
            throw new BusinessRuleViolationException(
                "A rate must be between 0 and 10 000 zł per person per night."
            );
        }

        // The deposit is charged on everyone, so it has to fit inside the cheaper
        // of the two rates or a kadra-heavy group would owe a deposit larger than
        // its price.
        if (deposit > Math.Min(price, supervisorPrice))
        {
            throw new BusinessRuleViolationException(
                "The deposit cannot be larger than the price itself."
            );
        }
    }

    private static PricingDefaultsDto ToDto(PricingDefaults d) =>
        new(
            d.PricePerPersonPerNightGrosze,
            d.SupervisorPricePerPersonPerNightGrosze,
            d.DepositPerPersonPerNightGrosze,
            d.UpdatedAt
        );
}

using CampCenter.Application.DTOs.AdminPanel;

namespace CampCenter.Application.Interfaces;

/// The centre's current rates. Reads fall back to the configured values until the
/// owner saves their own, so a fresh database prices bookings from day one.
public interface IPricingService
{
    Task<PricingDefaultsDto> GetAsync(CancellationToken cancellationToken = default);

    Task<PricingDefaultsDto> UpdateAsync(
        UpdatePricingDefaultsRequestDto request,
        CancellationToken cancellationToken = default
    );
}

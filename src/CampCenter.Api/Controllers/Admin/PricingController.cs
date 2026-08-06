using CampCenter.Application.DTOs.AdminPanel;
using CampCenter.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Admin;

/// The centre's rates. They prefill new bookings and quote the public site;
/// changing them never re-prices a group already on the books.
[ApiController]
[Authorize]
[Route("api/admin/pricing")]
public class PricingController : ControllerBase
{
    private readonly IPricingService _pricing;

    public PricingController(IPricingService pricing) => _pricing = pricing;

    [HttpGet]
    [ProducesResponseType(typeof(PricingDefaultsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken) =>
        Ok(await _pricing.GetAsync(cancellationToken));

    [HttpPut]
    [ProducesResponseType(typeof(PricingDefaultsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromBody] UpdatePricingDefaultsRequestDto request,
        CancellationToken cancellationToken
    ) => Ok(await _pricing.UpdateAsync(request, cancellationToken));
}

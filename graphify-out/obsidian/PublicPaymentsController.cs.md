---
source_file: "src/CampCenter.Api/Controllers/Public/PublicPaymentsController.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# PublicPaymentsController.cs

## Context

_Source: `src/CampCenter.Api/Controllers/Public/PublicPaymentsController.cs` (defined near L1; showing L1–L28 of 28)._

```csharp
using CampCenter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampCenter.Api.Controllers.Public;

/// Przelewy24 webhook endpoint. Anonymous by design — authenticity comes from
/// the SHA-384 signature (verified in the payment service) plus transaction/verify.
[ApiController]
[Route("api/public/payments")]
public class PublicPaymentsController : ControllerBase
{
    private readonly IPaymentService _payments;

    public PublicPaymentsController(IPaymentService payments) => _payments = payments;

    [HttpPost("p24/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> P24Status(
        [FromBody] GatewayNotification notification,
        CancellationToken cancellationToken
    )
    {
        await _payments.HandleNotificationAsync(notification, cancellationToken);
        return Ok();
    }
}
```

## Connections
- [[CampCenter.Api.Controllers.Public]] - `contains` [EXTRACTED]
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[PublicPaymentsController]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs
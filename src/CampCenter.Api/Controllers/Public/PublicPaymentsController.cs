// Przelewy24 webhook endpoint, switched off along with the rest of the online
// payment flow: groups now pay the centre directly and the owner records what
// arrived in the panel (Booking.PaymentState).
//
// Kept commented rather than deleted because card payment is expected back. To
// turn it on again, uncomment this file, the InitiatePayment action in
// PublicBookingsController, the IPaymentService / IPaymentGateway registrations
// in the two DependencyInjection files, and the P24Settings binding in Program.cs.
// PaymentService, Przelewy24Client and the Payments table are all still here.

// using CampCenter.Application.Interfaces;
// using Microsoft.AspNetCore.Mvc;
//
// namespace CampCenter.Api.Controllers.Public;
//
// /// Przelewy24 webhook endpoint. Anonymous by design — authenticity comes from
// /// the SHA-384 signature (verified in the payment service) plus transaction/verify.
// [ApiController]
// [Route("api/public/payments")]
// public class PublicPaymentsController : ControllerBase
// {
//     private readonly IPaymentService _payments;
//
//     public PublicPaymentsController(IPaymentService payments) => _payments = payments;
//
//     [HttpPost("p24/status")]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     [ProducesResponseType(StatusCodes.Status404NotFound)]
//     public async Task<IActionResult> P24Status(
//         [FromBody] GatewayNotification notification,
//         CancellationToken cancellationToken
//     )
//     {
//         await _payments.HandleNotificationAsync(notification, cancellationToken);
//         return Ok();
//     }
// }

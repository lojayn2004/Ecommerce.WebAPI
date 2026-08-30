using Ecommerce.Application.Dtos.Baskets;
using Ecommerce.Application.Dtos.Payment;
using Ecommerce.Application.ServicesAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace Ecommerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IPaymentService _paymentService, 
        IOptions<StripeOptions> _options): ApiBaseController
    {
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId)
        {
           var basket =  await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return ToActionResult<CustomerBasketDto>(basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebHook()
        {
            var bodyJson = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(bodyJson, Request.Headers["Stripe-Signature"], _options.Value.WebhookSecret);
                switch (stripeEvent.Type)
                {
                    case EventTypes.PaymentIntentSucceeded:
                        var succededPayment = stripeEvent.Data.Object as PaymentIntent;
                        if (succededPayment != null)
                            await _paymentService.PaymentSuccededAsync(succededPayment.Id);

                        break;
                    case EventTypes.PaymentIntentPaymentFailed:
                        var failedPayment = stripeEvent.Data.Object as PaymentIntent;
                        if (failedPayment != null)
                            await _paymentService.PaymentFailedAsync(failedPayment.Id);

                        break;

                }
                return Ok();

            }

            catch (StripeException ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}

using Ecommerce.Application.Dtos;
using Ecommerce.Application.Dtos.Payment;
using Ecommerce.Application.ServicesAbstractions;
using Microsoft.Extensions.Options;
using Stripe;

namespace Ecommerce.Infrastructure.Payment
{
    internal class StripePaymentGatway : IPaymentGatway
    {
        private readonly PaymentIntentService _paymentIntentService = new();
        
        public StripePaymentGatway(IOptions<StripeOptions> options)
        {
            StripeConfiguration.ApiKey = options.Value.SecretKey;

        }
        public async Task<PaymentIntentResult> CreatePaymentIntentAsync(decimal amount, string currency)
        {
            var options = new PaymentIntentCreateOptions()
            {
                Amount = (long)(amount * 100),
                Currency = currency.ToLower(),
                AllowedPaymentMethodTypes = ["card"]

            };
            var intent = await  _paymentIntentService.CreateAsync(options);

            return new PaymentIntentResult()
            {
                PaymentIntentId = intent.Id,
                ClientSecret = intent.ClientSecret
            };
        }

        public async Task<PaymentIntentResult> UpdatePaymentIntentAsync(string paymentIntentId, decimal amount)
        {
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = (long)(amount * 100),

            };
            var intent = await _paymentIntentService.UpdateAsync(paymentIntentId, options);
            return new PaymentIntentResult()
            {
                PaymentIntentId = intent.Id,
                ClientSecret = intent.ClientSecret
            };
        }
    }
}



namespace Ecommerce.Application.Dtos.Payment
{
    public class StripeOptions
    {
        public string SecretKey { get; set; }

        public string DefaultCurrency { get; set; }

        public string WebhookSecret {  get; set; } 
    }
}

using Ecommerce.Application.Dtos.Payment;


namespace Ecommerce.Application.ServicesAbstractions
{
    public interface IPaymentGatway
    {
        Task<PaymentIntentResult> CreatePaymentIntentAsync(decimal amount, string currency);

        Task<PaymentIntentResult> UpdatePaymentIntentAsync(string paymentIntentId, decimal amount);
    }
}

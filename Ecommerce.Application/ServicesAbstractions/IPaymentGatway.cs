using Ecommerce.Application.Dtos;


namespace Ecommerce.Application.ServicesAbstractions
{
    public interface IPaymentGatway
    {
        Task<PaymentIntentResult> CreatePaymentIntentAsync(decimal amount, string currency);

        Task<PaymentIntentResult> UpdatePaymentIntentAsync(string paymentIntentId, decimal amount);
    }
}

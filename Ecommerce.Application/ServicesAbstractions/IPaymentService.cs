using Ecommerce.Application.Dtos.Baskets;
using Ecommerce.Application.Dtos.ResultPattern;


namespace Ecommerce.Application.ServicesAbstractions
{
    public interface IPaymentService
    {
        Task<Result<CustomerBasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId);

        Task PaymentSuccededAsync(string paymentIntentId);

        Task PaymentFailedAsync(string paymentIntentId);

    }
}

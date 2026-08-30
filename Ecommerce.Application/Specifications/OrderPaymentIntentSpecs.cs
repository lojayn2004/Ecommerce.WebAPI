
using Ecommerce.Application.Dtos;
using Ecommerce.Domain.Models.Orders;

namespace Ecommerce.Application.Specifications
{
    internal class OrderPaymentIntentSpecs: BaseSpecification<Order, Guid>
    {
        public OrderPaymentIntentSpecs(string paymentIntentId):  base(o => o.PaymentIntentId == paymentIntentId)
        {

        }
    }
}

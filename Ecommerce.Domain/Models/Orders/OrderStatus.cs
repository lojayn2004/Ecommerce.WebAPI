

namespace Ecommerce.Domain.Models.Orders
{
    public enum OrderStatus
    {
        Pending = 0, 
        PaymentReceived,
        PaymentFailed
    }
}

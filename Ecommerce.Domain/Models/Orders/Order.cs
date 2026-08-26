using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Models.Orders
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {

        }

        public Order(string userEmail, OrderAddress shippingAddress,
                     DeliveryMethod deliveryMethod, IEnumerable<OrderItem> items, decimal subTotal)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            DeliveryMethodId = deliveryMethod.Id;
            Items = items;
            SubTotal = subTotal;
        }

        public string UserEmail { get; private set; } = default!;

        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.UtcNow;

        public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;

        public OrderAddress ShippingAddress { get; private set; } = default!;

        public int DeliveryMethodId { get; private set; }

        public DeliveryMethod DeliveryMethod { get; private set; } = default!;

        public IEnumerable<OrderItem> Items { get; private set; } = [];

        public decimal SubTotal { get; set; }

        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }

        public void MarkPaymnetRecieved()
        {
            OrderStatus = OrderStatus.PaymentFailed;
        }

        public void MarkPaymentRecieved()
        {
            OrderStatus = OrderStatus.PaymentReceived;
        }
    }
}
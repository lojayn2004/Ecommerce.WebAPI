
using Ecommerce.Application.Dtos.Auth;
using Ecommerce.Domain.Models.Orders;

namespace Ecommerce.Application.Dtos.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = default!;

        public DateTimeOffset OrderDate { get;  set; } = DateTimeOffset.UtcNow;

        public string OrderStatus { get; set; } = default!;

        public AddressDto ShippingAddress { get;  set; } = default!;

       
        public string DeliveryMethod { get;  set; } = default!;

        public IEnumerable<OrderItemDto> Items { get;  set; } = [];

        public decimal SubTotal { get; set; }

        public decimal DeliveryCost { get; set; }

        public decimal Total { get; set; }

    }
}

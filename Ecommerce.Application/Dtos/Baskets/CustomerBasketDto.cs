

namespace Ecommerce.Application.Dtos.Baskets
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }

        public string? PaymentIntentId { get; set; } = default;

        public string? ClientSecret { get; set; } = default;

        public int? DelievryMethodId { get; set; } = default;

        public decimal? ShippingPrice { get; set; } = default;


        public ICollection<BasketItemDto> BasketItems { get; set; }
    }
}



namespace Ecommerce.Domain.Models.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; } = string.Empty;

        public string? PaymentIntentId { get; set; } = default;

        public string? ClientSecret { get; set; } = default;

        public int? DelievryMethodId { get; set; } = default;

        public decimal? ShippingPrice { get; set; } = default;


        public ICollection<BasketItem> BasketItems { get; set; } = [];

    }
}

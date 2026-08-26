

namespace Ecommerce.Application.Dtos.Baskets
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }

        public ICollection<BasketItemDto> BasketItems { get; set; }
    }
}


namespace Ecommerce.Domain.Models.Basket
{
    public class BasketItem
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public string PictureURL { get; set; } = string.Empty;
        public int Quantity { get; set; }


    }
}

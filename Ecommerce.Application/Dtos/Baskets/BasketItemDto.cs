

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Dtos.Baskets
{
    public class BasketItemDto
    {
        [Required(ErrorMessage = "Product Id is Required")]
        public int Id { get; set; }

        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        public string ProductName { get; set; } = string.Empty;


        public string PictureURL { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}

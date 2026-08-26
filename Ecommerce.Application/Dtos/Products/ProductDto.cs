
namespace Ecommerce.Application.Dtos.Products
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; } = "";

        public string PictureUrl { get; set; } = "";

        public decimal Price { get; set; }


        public BrandDto ProductBrand { get; set; }
       
        public TypeDto ProductType { get; set; }
      

    }
}

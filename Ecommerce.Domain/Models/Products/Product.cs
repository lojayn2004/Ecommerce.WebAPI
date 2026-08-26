

using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Models.Products
{
    public class Product: BaseEntity<int> 
    {
        public string Description { get; set; } = "";

        public string PictureUrl { get; set; } = "";

        public decimal Price { get; set; }


        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }

        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }


    }
}

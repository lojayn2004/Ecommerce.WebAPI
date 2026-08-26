namespace Ecommerce.Application.Dtos.Products
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string? SearchValue { get; set; }
    }
}

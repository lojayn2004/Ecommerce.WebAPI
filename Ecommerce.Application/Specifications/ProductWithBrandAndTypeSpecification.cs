using Ecommerce.Application.Dtos.Products;
using Ecommerce.Domain.Models.Products;
using System.Linq.Expressions;

namespace Ecommerce.Application.Specifications
{
    internal class ProductWithBrandAndTypeSpecification: BaseSpecification<Product, int>
    {
        public ProductWithBrandAndTypeSpecification(): base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        public ProductWithBrandAndTypeSpecification(ProductQueryParams query) : base(GetExpressionFromQuery(query))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
           
        }


        public ProductWithBrandAndTypeSpecification(int productId) : base(p => productId == p.Id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        private static Expression<Func<Product, bool>> GetExpressionFromQuery(ProductQueryParams query)
        {
            return p => (query.BrandId == null || p.ProductBrandId == query.BrandId)
                     && (query.TypeId == null || p.ProductTypeId == query.TypeId)
                     && (string.IsNullOrWhiteSpace(query.SearchValue) || p.Name.ToLower().Contains(query.SearchValue.ToLower()));
        }

    }
}

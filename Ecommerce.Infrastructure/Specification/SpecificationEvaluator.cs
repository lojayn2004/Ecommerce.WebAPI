using Ecommerce.Domain.Common;
using Ecommerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;

namespace Ecommerce.Infrastructure.Specification
{
    public static class SpecificationEvaluator
    {
        public static  IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecification<TEntity, Tkey> specs) where TEntity: BaseEntity<Tkey>
        {
            var query = inputQuery;

            if(specs.Criteria != null) 
                query = query.Where(specs.Criteria);

            if (specs?.IncludeExpressions != null)
            {
                foreach (var expr in specs.IncludeExpressions)
                {
                    query = query.Include(expr);
                }
            }
            return  query;


        }
    }
}

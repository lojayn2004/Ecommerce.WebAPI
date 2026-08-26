

using Ecommerce.Domain.Common;
using Ecommerce.Domain.Contracts;
using System.Linq.Expressions;

namespace Ecommerce.Application.Specifications
{
    internal abstract class BaseSpecification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; private set; } = new List<Expression<Func<TEntity, object>>>();

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            IncludeExpressions.Add(include);
        }


    }
}

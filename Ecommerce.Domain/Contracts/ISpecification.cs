using Ecommerce.Domain.Common;
using System.Linq.Expressions;


namespace Ecommerce.Domain.Contracts
{
    public interface ISpecification<TEntity, Tkey> where TEntity: BaseEntity<Tkey>
    
    {
        ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        Expression<Func<TEntity, bool>> Criteria { get; }
    }
}

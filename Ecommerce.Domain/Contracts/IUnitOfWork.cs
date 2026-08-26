

using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Contracts
{
    public interface  IUnitOfWork
    {

        IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity: BaseEntity<Tkey>;


        Task<int> SaveAllChangesAsync();
    }
}

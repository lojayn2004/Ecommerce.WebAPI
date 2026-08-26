

using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Contracts
{
    public interface  IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> spec);

        Task<TEntity?> GetById(Tkey id);

        Task<TEntity?> GetById(ISpecification<TEntity, Tkey> spec);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

    }
}

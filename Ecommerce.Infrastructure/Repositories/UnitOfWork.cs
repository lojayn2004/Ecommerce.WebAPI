

using Ecommerce.Domain.Common;
using Ecommerce.Domain.Contracts;
using Ecommerce.Infrastructure.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class UnitOfWork(StoreDbContext _storeDbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repos = [];
        
        
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
           
            string repoName = typeof(TEntity).Name;

            if (!repos.ContainsKey(repoName))
                repos[repoName] = new GenericRepository<TEntity, Tkey>(_storeDbContext);
            return (IGenericRepository<TEntity, Tkey>)repos[repoName];

        }

        public async Task<int> SaveAllChangesAsync()
        {
            return await _storeDbContext.SaveChangesAsync();
        }
    }
}

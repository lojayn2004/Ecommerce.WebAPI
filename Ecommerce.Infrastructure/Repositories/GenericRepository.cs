using Ecommerce.Application.Specifications;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Contracts;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



namespace Ecommerce.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity, Tkey>(StoreDbContext _storeDbContext) :
        IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public void Add(TEntity entity)
        {
            _storeDbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _storeDbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _storeDbContext.Set<TEntity>().ToListAsync();

        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> spec)
        {
            var query = SpecificationEvaluator.CreateQuery(_storeDbContext.Set<TEntity>(), spec);
            Console.WriteLine("Query Created Successfully !");
            return await query.ToListAsync();
        }

        public  async Task<TEntity?> GetById(Tkey id)
        {
            return await _storeDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> GetById(ISpecification<TEntity, Tkey> spec)
        {
            var query = SpecificationEvaluator.CreateQuery(_storeDbContext.Set<TEntity>(), spec);
            return await query.FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            _storeDbContext.Set<TEntity>().Update(entity);
        }
    }
}

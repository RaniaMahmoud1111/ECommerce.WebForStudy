using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()=> await _dbContext.Set<TEntity>().ToListAsync();


        public async Task<TEntity> GetByIdAsync(TKey id)=>await _dbContext.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity)
        {
           _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        //
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification)
        {
          return  await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification).ToListAsync();
        }


        public async Task<TEntity> GetByIdAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification).FirstOrDefaultAsync();

        }

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> specifications)
        {
            // here we use same creat query but it differe depends on that specifications be passed to it here not applay paginations means no take , skip in query 
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(),  specifications).CountAsync();

        }
    }
}

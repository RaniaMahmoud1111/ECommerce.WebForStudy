using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity :BaseEntity<TKey>
    {
        // i add  signature for methods that help me to pass specifications and create query depends on  expressions that will passed 
        // here we have function over load as we have one than one func has same name but recive diferen parameters
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity,TKey> specification);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetByIdAsync(ISpecification<TEntity,TKey> specification);

        Task AddAsync(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);

        Task<int> CountAsync(ISpecification<TEntity, TKey> specifications);//make it async as i will use countAsync inside it 

    }
}

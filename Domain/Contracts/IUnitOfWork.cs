using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity ,TKey> GetRepository<TEntity, TKey>() where TEntity:BaseEntity<TKey>;
        Task <int> SaveChangesAsync();


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    // make unit of work deal with generic repos not each repo seperatly 
    // it create obj of type TEntity and put it in dictionary as you may ask for it later 
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string,object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //need to control on generation of generic repos by that method 
            // get type name
            var typeName=typeof(TEntity).Name;
            //dictionary<string, object>===>string key name of entity , object is the value that is the object from generic repository of TEntity 

            if (_repositories.ContainsKey(typeName))
                return (IGenericRepository<TEntity,TKey>) _repositories[typeName];// here we cast the object to IGenericRepository

            else
            {
                //create object of that TEntity 
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);

                // then store obj in the dictionary 
                //  _repositories.Add(typeName, repo);
                _repositories[typeName] = repo;
                return repo;// return object 

            }
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }



    }
}

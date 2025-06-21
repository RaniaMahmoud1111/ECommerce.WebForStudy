using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    // that will hold the code logic 
    public static class SpecificationEvaluator
    {
        // why retun type IQueryable as i need the filteration that will appled happened in database 
        // inputQuery represenet  base query   _dbContext.Set<TEntity>()
        //   specification like that   .Where(specification.Criteria).Include(specification.IncludeExpression[0]); (i will check on it first)  

        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecification<TEntity,Tkey>? specification)
            where TEntity:BaseEntity<Tkey>
            {
            // here i will build my query dynamic 

            var query = inputQuery;//  _dbContext.Set<TEntity>()
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);// _dbContext.Set<TEntity>().where(p=>p.id==1)


           
            // we can creat query by foreach or by linq operator called aggregate that make aqumlation on query let `s type the two ways now 
            
            // 1. by foreach 
            //foreach(var include in specification.IncludeExpression)
            //{
            //    query=query.Include(include);
            //}
            //_dbContext.Set<TEntity>().where(p => p.id == 1).Include(p => p.ProductType).Include(p => p.ProductBrand);

            // 2. by linq operator 
            query = specification.IncludeExpression.Aggregate(query, 
                (currentQuery,include)=>currentQuery.Include(include));
           // _dbContext.Set<TEntity>().where(p => p.id == 1).Include(p => p.ProductType).Include(p => p.ProductBrand);


            if(specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
            else if(specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);


            // add operator 
            if (specification.IsPaginated == true)
                query = query.Skip(specification.Skip).Take(specification.Take);

            return query;

        }

    }
}

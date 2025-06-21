using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ISpecification<TEnetity,T> where TEnetity : BaseEntity<T>
    {
        Expression<Func<TEnetity,bool>> Criteria  { get; }// criteria for where that take expressio abd retrun   bool , criateria may be null like getall not need criateria 

        List<Expression<Func<TEnetity,object>>> IncludeExpression { get; }

        // for sorting
        Expression<Func<TEnetity,object>> OrderBy {  get; }
        Expression <Func<TEnetity,object>> OrderByDescending { get; }

        int Skip { get; }
        int Take { get; }
        bool IsPaginated { get; }// 
    
    }
} 

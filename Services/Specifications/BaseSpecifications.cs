using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Services.Specifications
{// that class contain base specifictaions like(criteria , include(will be of type list of object as i not know size will come and of what  ))
    public  class BaseSpecifications<TEnetity,T> : ISpecification<TEnetity,T> where TEnetity : BaseEntity<T>
    {
        
        public BaseSpecifications(Expression<Func<TEnetity, bool>>?criteria )// take expre of func of one param return bool represent criateria will be nullable as it may be null 
        {
            Criteria = criteria;
        }

        // example
        // if i deal with get all the where(Criteria) will be null   
        // if deal with get by id i will have where(Criteria)
        public Expression<Func<TEnetity, bool>> Criteria
        {
            get;
            private set;// i not need to set it from outerside 
        }
        public List<Expression<Func<TEnetity, object>>> IncludeExpression { get; } = [];// initailize it to avoid null reference exception , so i make it refer to null object  

        public Expression<Func<TEnetity, object>> OrderBy { get; private set; }

        public Expression<Func<TEnetity, object>> OrderByDescending { get; private set; }

        public int Skip{ get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddIncludes(Expression<Func<TEnetity, object>> include) 
            => IncludeExpression.Add(include);// that will fill the Include
        protected void AddOrderBy (Expression<Func<TEnetity , object>>orderBy)
            =>OrderBy=orderBy;
        protected void AddOrderByDescending (Expression<Func<TEnetity,object>> orderByDescending)
            =>OrderByDescending=orderByDescending;
    
        protected void ApplayPagination(int pageSize, int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip=(pageIndex-1)*pageSize;

        }


    }

}

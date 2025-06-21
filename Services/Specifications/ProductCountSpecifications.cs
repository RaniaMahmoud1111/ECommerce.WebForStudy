using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ProductModule;
using Shared.DataTransferObject.ProductModuleDtos;

namespace Services.Specifications
{
    public class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParameters  parameters) : base(ApplayCriteria(parameters))
        {



        }
        //here i need that criateria without paginations to get the total count 
        private static Expression<Func<Product, bool>> ApplayCriteria(ProductQueryParameters parameters)
        {
            return product =>
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId) &&
                    (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId) &&
                    (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower()));
        }
    }
}

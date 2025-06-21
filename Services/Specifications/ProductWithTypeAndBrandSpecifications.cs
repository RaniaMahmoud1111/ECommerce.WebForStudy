using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ProductModule;
using Shared.DataTransferObject.ProductModuleDtos;

namespace Services.Specifications
{
    public class ProductWithTypeAndBrandSpecifications:BaseSpecifications<Product,int>
    {

        // chain on ctor  that take criteria 
        // use this ctor to create query to get product by id 
        public ProductWithTypeAndBrandSpecifications(int id)
            :base(product=>product.Id==id)
        {
            // Add includes to get related data 
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);

        }



        // use this ctor to create query to get all product 
        // i use that ctor if i need to  get all product and also if i need to applay some filterations  
        // ? means it is nullable :get all with out any filterations : not pass brandId , typeId 
        public ProductWithTypeAndBrandSpecifications(ProductQueryParameters parameters) : base(ApplayCriteria(parameters))
        {
            // Add includes to get related data 
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
            ApplaySorting(parameters);
            ApplayPagination(parameters.PageSize,parameters.PageIndex);

        }

        private static Expression<Func<Product,bool>> ApplayCriteria(ProductQueryParameters parameters)
        {
            return product => 
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId) &&
                    (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId) &&
                    (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower()));
        }

        private void ApplaySorting(ProductQueryParameters parameters)
        {
            switch (parameters.Options)
            {

                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
        }
    }
}

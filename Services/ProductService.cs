using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Execptions;
using Domain.Models.ProductModule;
using Services.Specifications;
using ServicesAbstractions;
using Shared;
using Shared.DataTransferObject.ProductModuleDtos;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        // Service => unitOfWork => GenericRepo => Repo =>  
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            // this will call get all in repo so we need obj from repo but wait we work generic so we many need obj from generic repo  or obj from unit of work(which deals with generic repo) as we not deal with generic repo direct 
             var repo = _unitOfWork.GetRepository<ProductBrand, int>();// here we mak obj from generic repo of ProductBrand
               var Brands=  await repo.GetAllAsync();
            //we need casting as the method return BrandDto and the obj you create of type ProductBrand SO we need mapping(ProductBrand <=> BrandDto)
           // var BrandsDto= _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);// here we put the source an d Dest 
            var BrandsDto= _mapper.Map<IEnumerable<BrandDto>>(Brands);//the Map will know the source from the obj you passed as a parameter 
             return BrandsDto;

        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDto>>(types);// know the src from passed parameter 
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            // here we will use the new version of GetByIdAsync that take specifications 
            var specifications=new ProductWithTypeAndBrandSpecifications(id);
            var product =await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
            // here may exception happend as id may be not found so let`s check on  returned product 
            if(product is null)
            {
                throw new ProductNotFoundException(id);
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductQueryParameters parameters)
        {
            // i not use the normal verion of GetAllProductsAsync as we implement one that take specification  it creat query by it self 
            // let`s go to use it 
            var specifications = new ProductWithTypeAndBrandSpecifications(parameters);

            var repo =  _unitOfWork.GetRepository<Product, int>();
            var products = await repo.GetAllAsync(specifications);// all products  , that specifications contpaginations 
          
            var Data= _mapper.Map<IEnumerable<ProductDto>>(products);
            var ProductCount = products.Count();
            var CountSpec = new ProductCountSpecifications(parameters);
            var totalCount = await repo.CountAsync(CountSpec);
            return new PaginationResponse<ProductDto>(parameters.PageIndex, ProductCount, totalCount, Data);
        }

       
        

    }
}

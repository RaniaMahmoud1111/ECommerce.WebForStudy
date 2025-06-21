using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.ProductModule;
using Shared.DataTransferObject.ProductModuleDtos;

namespace Services.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, options => options.MapFrom(src => src.ProductType.Name))

                .ForMember(dest => dest.PictureUlr, options => options.MapFrom(src => src.PictureUrl));
                

            CreateMap<ProductBrand,BrandDto>();
            CreateMap<ProductType,TypeDto>();


        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Execptions;
using Domain.Models.BasketModule;
using ServicesAbstractions;
using Shared.DataTransferObject.BasketModuleDtos;

namespace Services
{
    // here i will deal with repository so i ask clr to inject obj of basket repo 
    // that baskeRepo deal with basket model so we need mapping between basketDto and CustomerBasket
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        // take basket dto then pass to create or update in repo so we need mapping 
        public async Task<BasketDto> CreateOrUpdateBasket(BasketDto basket)
        {
            var CustomerBasket = mapper.Map<BasketDto, CustomerBasket>(basket);
            var CreatedOrUpdatedBasket=await basketRepository.CreateOrUpdateAsync(CustomerBasket);
            if(CreatedOrUpdatedBasket !=null)// exist
            {
                // call get basket to return basket after create or update
                return await GetBasketAsync(basket.Id);
            }
            else// 
            {
                throw new Exception("Can not Create Or Update Basket Now");
            }

        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
         return   await basketRepository.DeleteBasketAsync(key);
        }

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            // it may retturn basket or null
           var Basket=await  basketRepository.GetBasketAsync(key);
            if(Basket!=null)// basket exist then mapp that obj 
            {
                return mapper.Map<CustomerBasket, BasketDto>(Basket);
            }
            else// basket not found 
            {
                throw new BasketNotFoundException(key);
            }
        
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObject.BasketModuleDtos;

namespace ServicesAbstractions
{
    public interface IBasketService
    {
        // service that get basket ,return basket Dto
        Task<BasketDto> GetBasketAsync(string key);

        Task<BasketDto> CreateOrUpdateBasket(BasketDto basket);

        Task<bool> DeleteBasketAsync(string key);

    }
}

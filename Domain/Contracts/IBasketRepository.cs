using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.BasketModule;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        //Get
        Task<CustomerBasket> GetBasketAsync(string key);
        // create if not exist  , upddate
        Task<CustomerBasket> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan? TimeToLive = null);

        Task<bool> DeleteBasketAsync(string key);
    
    
    }
}

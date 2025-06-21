using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.BasketModule;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database=connection.GetDatabase();


        public async Task<CustomerBasket> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            //convert to json by serialize
            var JsonBasket = JsonSerializer.Serialize(basket);
       var ISCreatedOrUpdated=  await   _database.StringSetAsync(basket.Id,JsonBasket,TimeToLive??TimeSpan.FromDays(30));// count 30 days from now
            if (ISCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
                return null;
        }



        public async Task<bool> DeleteBasketAsync(string key)
        {
            // i need to catch db of redis by connection multiplexer
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            // get json obj in redis so need deserialize to basket obj 
            var Basket=await _database.StringGetAsync(key);
          // here i cannot check is null as basket is redis value that is struct which cannot allow null 

            if(Basket.IsNullOrEmpty)
            {
                return null;
            }
            else// deserialize to basket obj
            {
               return  JsonSerializer.Deserialize<CustomerBasket>(Basket!);
            }


        }
    }
}

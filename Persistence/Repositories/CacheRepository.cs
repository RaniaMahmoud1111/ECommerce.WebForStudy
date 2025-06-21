using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Services;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    // to deals weith redis we need to create obj of IConnectionMultiplexer
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase(); 

        //Get cacheed data 
        public async Task<string?> GetAsync(string cacheKey)
        {

            var cachevalue = await _database.StringGetAsync(cacheKey);
            return  cachevalue.IsNullOrEmpty ? null : cachevalue.ToString();
        }

        //set data to be cached 
        public  async Task SetAsync(string cacheKey, string value, TimeSpan timeSpan)
        {

         await   _database.StringSetAsync(cacheKey,value,timeSpan);
            
        }
    }
}

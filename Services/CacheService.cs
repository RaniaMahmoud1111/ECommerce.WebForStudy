using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ServicesAbstractions;

namespace Services
{
    //service=>Repository 
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {

        public async Task<string?> GetAsync(string cachKey)
        {

           return await cacheRepository.GetAsync(cachKey);
        }

        public async Task SetAsync(string cachKey, object cacheValue, TimeSpan timeToLive)
        {
            // need to serialize value from obj to string 
            var value = JsonSerializer.Serialize(cacheValue);
           await cacheRepository.SetAsync(cachKey,value,timeToLive);
        
        }
    }
}

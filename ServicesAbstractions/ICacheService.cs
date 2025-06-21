using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractions
{
    public interface ICacheService
    {

        //Get
        Task<String?> GetAsync(string cachKey);

        //Set 
        Task SetAsync(string cachKey ,object value , TimeSpan timeToLive);


    }
}

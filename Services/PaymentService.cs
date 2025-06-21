using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesAbstractions;
using Shared.DataTransferObject.BasketModuleDtos;

namespace Services
{
    public class PaymentService : IPaymentService
    {

        public Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            throw new NotImplementedException();
        }

        //Config stripe :install package stripe.net 
        //Get basket by basketId
        //Get Amount => Get Product + method Cost 
        //create payment Intent [Create or update ]



    }
}

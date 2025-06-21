using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared.DataTransferObject.BasketModuleDtos
{
    // always in service we deal with Dtos as it may like model tepical 
    public  class BasketDto
    {

        public string Id { get; set; }

        public ICollection<BasketItemDto> Items { get; set; }

        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public  decimal? ShippingPrice { get; set; }


    }
}

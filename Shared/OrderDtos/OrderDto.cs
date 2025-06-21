using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.IdentityDtos;

namespace Shared.OrderDtos
{
    public  class OrderDto
    {
        public string BasketId { get; set; } = default;

        public int DeliveryMethodId { get; set; }

        public AddressDto ShipToAddress { get; set; } = default;



    }
}

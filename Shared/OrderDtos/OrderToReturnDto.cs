using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.IdentityDtos;

namespace Shared.OrderDtos
{
    public  class OrderToReturnDto
    {
        public Guid Id { get; set; }

        public string BbuyerEmail { get; set; } = default;

        public DateTimeOffset OrderDate { get; set; }

        public AddressDto ShipToAddress { get; set; } = default!;

        public string DeliveryMethod { get; set; } = default!;
        public string Status { get; set; } = default!;

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal deliveryCost { get; set; }

        public  ICollection<OrderItemDto> Items { get; set; }

    }
}

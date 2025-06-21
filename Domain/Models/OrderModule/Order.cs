using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string uSerEmail, ICollection<OrderItem> items, OrderAddress address, DeliveryMethod deliveryMethod, decimal subTotal)
        {
            USerEmail = uSerEmail;
            Items = items;
            Address = address;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }


        public string USerEmail { get; set; }

        public OrderAddress Address { get; set; } = default;
        public DeliveryMethod DeliveryMethod { get; set; } = default;
        public decimal SubTotal { get; set; }

        public ICollection<OrderItem> Items { get; set; } = [];//empty list 


        //public decimal Total  { get; set; }//derived attribute be    [NotMapped] be calcolatedmin runtime 

        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
        public  OrderStatus OrderStatus { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public int DeliveryMethodId { get; set; }//fk

    }
}

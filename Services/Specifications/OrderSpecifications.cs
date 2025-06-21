using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModule;

namespace Services.Specifications
{
    public class OrderSpecifications:BaseSpecifications<Order,Guid>
    {
        //chain on ctor of  base
        public OrderSpecifications(string email ):base(o=>o.USerEmail==email)
        {
            //related data need include 
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o=>o.Items);
        }

        public OrderSpecifications(Guid id):base(o=>o.Id==id)
        {
            //related data need include 
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.Items);
        }

    }
}

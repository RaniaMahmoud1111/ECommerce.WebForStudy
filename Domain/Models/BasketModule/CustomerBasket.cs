using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BasketModule
{
    //i not inherit from base class here as i not need to store that model in db 
     // as not all users comes and add items to their cards i will not store them i not atore unused data to improve my db 
    public class CustomerBasket
    {

        public string Id { get; set; }//Guid created from client frontend 

        public ICollection<BasketItem> Items { get; set; }
    }
}

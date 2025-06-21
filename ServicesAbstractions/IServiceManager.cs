using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractions
{
    public interface IServiceManager
    {
        // put signature for each service in your system 
        public IProductService productService { get;  }
         public IBasketService BasketService { get; }
         public IAuthenticationService AuthenticationService { get; }

        public IOrderService OrderService { get; }


    }
}

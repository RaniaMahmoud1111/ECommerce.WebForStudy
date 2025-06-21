using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesAbstractions;

namespace Services
{// we make that refactory to increase the performance when nof services increases and i not need to create object manaual as i use lazy implementation   
    // ask CLR TO Create objects if needed
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> ProductFactory,Func<IBasketService> BasketFactory,
        Func<IAuthenticationService> AuthenticationFactory,Func<IOrderService> OrderFactory
        ) : IServiceManager
    {
        public IProductService productService => ProductFactory.Invoke();

        public IBasketService BasketService => BasketFactory.Invoke();

        public IAuthenticationService AuthenticationService => AuthenticationFactory.Invoke();

        public IOrderService OrderService => OrderFactory.Invoke();
    }
}

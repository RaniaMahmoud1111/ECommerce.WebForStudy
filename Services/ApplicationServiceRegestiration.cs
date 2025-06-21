using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using ServicesAbstractions;

namespace Services
{
    public static class ApplicationServiceRegestiration
    {
           
        public static IServiceCollection AddApplicationServices( this IServiceCollection Services)
        {// i not need builder as i can deal with services direct 
            Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            Services.AddScoped<IProductService, ProductService>();//Allow DI
            Services.AddScoped<Func<IProductService>>(provider =>
            ()=> provider.GetRequiredService<IProductService>());

            Services.AddScoped<IBasketService, BasketService>();//Allow DI
            Services.AddScoped<Func<IBasketService>>(provider=>
            ()=> provider.GetRequiredService<IBasketService>());

            Services.AddScoped<IAuthenticationService,AuthenticationService>();//Allow DI 
            Services.AddScoped<Func<IAuthenticationService>>(provider=>
            ()=>provider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IOrderService, OrderService>();//allow DI
            Services.AddScoped<Func<IOrderService>>(provider=>
            ()=>provider.GetRequiredService<IOrderService>());


            Services.AddScoped<ICacheService, CacheService>();

            return Services;
        }

       
    }
}

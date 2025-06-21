using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using Services;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServicesResgestiration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                //Configuration == appsetting , GetConnectionString func search of ConnectionString section  by its name 
                var connectiontring = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectiontring);
            });


            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();// register it to inject obj of unitofwork in services
            Services.AddScoped<ICacheRepository, CacheRepository>();
            Services.AddScoped<IBasketRepository,BasketRepository>();
            //redis connection
            Services.AddScoped<IConnectionMultiplexer> ( (_)=>
                {
                    return  ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnection"));
                });

            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                //Configuration == appsetting , GetConnectionString func search of ConnectionString section  by its name 
                var connectiontring = Configuration.GetConnectionString("IdentityConnection");
                options.UseSqlServer(connectiontring);
            });


            Services.AddIdentityCore<ApplicationUser>(options =>
            {

            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();


            return Services ;
        }

        //public static IServiceProvider AddDataSeedService(this IServiceProvider Services)
        //{

        //    using var scope = Services.CreateScope();
        //    var ObjectForDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
        //    ObjectForDataSeeding.DataSeed();
        //    return Services;
        //}

    }
}

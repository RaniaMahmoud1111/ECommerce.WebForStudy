using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext,UserManager<ApplicationUser> _userManager,RoleManager<IdentityRole> _roleManager, StoreIdentityDbContext _identitydbContext) : IDataSeeding
    {
        public void DataSeed()
		{
			try
			{   // check if any migrations not applayed yet 
				if (_dbContext.Database.GetPendingMigrations().Any())
				{
					// yes the applay them 
					_dbContext.Database.Migrate();
				}
				// seed Brands data 
				if (!_dbContext.ProductBrands.Any())// check if table empty then seed the data 
				{
					var ProductBrandData = File.ReadAllText(@"..\Persistence\Data\DataSeed\brands.json");
					var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
					if (ProductBrands != null && ProductBrands.Any())// if list  has data then add them in table in db 
					{
						_dbContext.ProductBrands.AddRange(ProductBrands);// state be added 
					}
				}

				//seed Types data 
				if (!_dbContext.ProductTypes.Any())// check if table empty then seed data in table 
				{
					var ProductTypeData = File.ReadAllText(@"..\Persistence\Data\DataSeed\types.json");// get json data 
					var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);// deserialize json data to C# objects 			
					if(ProductTypes!=null && ProductTypes.Any())
					{
						_dbContext.ProductTypes.AddRange(ProductTypes);
					}
				
				}

                //seed Product data 
                if (!_dbContext.Products.Any())//check if table empty then seed data in it 
				{
					var ProductData = File.ReadAllText(@"..\Persistence\Data\DataSeed\products.json");
					var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
					if(Products!=null && Products.Any())
					{
						_dbContext.Products.AddRange(Products);
					}
				}

				//seed delivery data 
				if(!_dbContext.Set<DeliveryMethod>().Any())//check if table empty then seed data in it 
				{
					var DeliveryMethodData = File.ReadAllText(@"..\Persistence\Data\DataSeed\delivery.json");
					var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
					if(DeliveryMethods != null && DeliveryMethods.Any())
					{
						_dbContext.Set<DeliveryMethod>().AddRange(DeliveryMethods);
					}
				}

				_dbContext.SaveChanges();

			}
			catch (Exception ex)
			{
				//todo
			}
        }

        public async  void IdentityDataSeed()
        {

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

            }

            if (!_userManager.Users.Any())
            {
                try
                {
                    var user01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Ahmed",
                        PhoneNumber = "1234567890",
                        UserName = "MohamedAhmed"

                    };
                    var user02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Ahmed",
                        PhoneNumber = "1234567890",
                        UserName = "SalmaAhmed"

                    };

                    await _userManager.CreateAsync(user01, "P@$$w0rd");
                    await _userManager.CreateAsync(user02, "P@$$w0rd");

                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                    await _identitydbContext.SaveChangesAsync();

                }
                catch (Exception)
                {

                    throw;
                }
            }

        }


            

    }
    }

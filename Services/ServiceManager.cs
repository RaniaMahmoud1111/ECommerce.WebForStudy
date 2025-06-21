using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServicesAbstractions;

namespace Services
{
    // here we need obj of unitOfWork , Mapper
    // in unitOfWork we handle creation of obj 1.by method GetRepo and on passed type it will create obj on that type passed   public class ServiceManager : IServiceManager
  // here we will handle creation of obj by lazy implementation
  // lazy means when i ask, it will create 
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper, IBasketRepository _basketRepository,UserManager<ApplicationUser> _userManager,IConfiguration _configuration) 
    {
        private readonly Lazy<IProductService> _LaxzyProductService = new Lazy<IProductService>(()=> new ProductService(_unitOfWork,_mapper));

        private readonly Lazy<IBasketService> _LaxzyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));

        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _configuration, _mapper));

        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper,_basketRepository, _unitOfWork));


        public IProductService productService => _LaxzyProductService.Value;
        public IBasketService BasketService => _LaxzyBasketService.Value;
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;
        public IOrderService OrderService => _LazyOrderService.Value;
    }
}

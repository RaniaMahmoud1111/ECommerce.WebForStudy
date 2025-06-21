using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Execptions;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Services.Specifications;
using ServicesAbstractions;
using Shared.IdentityDtos;
using Shared.OrderDtos;

namespace Services
{
    public class OrderService(IMapper _mapper,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            // map address to order address
            var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.ShipToAddress); 
         
            //Get basket 
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId) ??
                throw new BasketNotFoundException(orderDto.BasketId);


            //create order Items List 
            List<OrderItem> OrderItems = [];//collection initialization
            var ProductRepo =  _unitOfWork.GetRepository<Product, int>();

            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrder()
                    {
                        ProductId = Product.Id,
                        PictureUrl = Product.PictureUrl,
                        ProductName = Product.Name,

                    },
                    Price = Product.Price,// not from front 
                    Quantity = item.Quantity,

                };
                OrderItems.Add(orderItem);
            }

            //get Delivery Method by Id 
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            //Calc sub total
            var SubTotal = OrderItems.Sum(I => I.Quantity * I.Price);

            //Finally we can create Order !!!
            var Order = new Order(email,OrderItems,OrderAddress, DeliveryMethod, SubTotal);


            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);

            await _unitOfWork.SaveChangesAsync();// 

            // return _mapper.Map<Order, OrderToReturnDto>(Order);
             return _mapper.Map<OrderToReturnDto>(Order);



        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email)
        {
            var spec=new OrderSpecifications(email);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            
            return _mapper.Map<IEnumerable<Order>,IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>,IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
            
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
           var spe=new OrderSpecifications(id);
            var order=await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spe);
       
            return _mapper.Map<Order,OrderToReturnDto>(order);

        }
    }
}

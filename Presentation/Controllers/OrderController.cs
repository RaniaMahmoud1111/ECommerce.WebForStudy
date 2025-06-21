using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.OrderDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] // api/Order 
    public  class OrderController(IServiceManager _serviceManager):ControllerBase
    {
        //Create Order 
        [Authorize]
        [HttpPost]//Post api/Order/

       public async Task<ActionResult<OrderToReturnDto>>CreateOrderAsync(OrderDto orderDto)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var order = await _serviceManager.OrderService.CreateOrderAsync(orderDto,email);

            return Ok(order);

        }

        //GetOrder By Id  endpoint 
        [HttpGet("{id:guid}")]// Get api/Order/
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }


        //Get All Orders EndPoint
        [Authorize]
        [HttpGet]//Get api/Order
        public  async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var email=User.FindFirst(ClaimTypes.Email).Value;
            var orders=await _serviceManager.OrderService.GetAllOrdersAsync(email);
            return Ok(orders);
        }

        //Get DeliveryMethod EndPoint
        [HttpGet("DeliveryMethod")]//Get api/Order/DeliveryMethod
        public  async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var result = await _serviceManager.OrderService.GetDeliveryMethodAsync();
            return Ok(result);
        }

    }
}

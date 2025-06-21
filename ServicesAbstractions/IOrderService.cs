using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Shared.OrderDtos;

namespace ServicesAbstractions
{
    public interface IOrderService
    {
        //Create Order
        // Creating Order Will Take Basket Id, Shipping Address , Delivery Method Id , Customer Email
        // Return Order Details(Id , UserName , OrderDate , Items (Product Name - Picture Url - Price - Quantity)
        //  Address , Delivery Method Name , Order Status Value , Sub Total, Total Price  )

        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto , string email );
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();

        //Get All orders
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);

    }
}

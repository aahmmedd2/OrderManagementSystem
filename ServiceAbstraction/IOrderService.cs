using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO_S;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto);
        Task<OrderToReturnDto?> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync();
        Task<bool> UpdateOrderStatusAsync(Guid orderId, string newStatus);
        Task<bool> UpdateOrderAsync(Guid orderId, OrderDto orderDto);
    }
}

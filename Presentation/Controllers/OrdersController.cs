using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTO_S;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrdersController(IServiceManager _serviceManager) : ControllerBase
    {
        
        [HttpPost] // POST: /api/orders
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder([FromBody] OrderDto orderDto)
        {
            var OrderCreated = await _serviceManager.OrderService.CreateOrderAsync(orderDto);
            
            return Ok(OrderCreated);
        }
        
        [HttpGet("{orderId:guid}")] // GET: /api/orders/{orderId}
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid orderId)
        {
            var Order = await _serviceManager.OrderService.GetOrderByIdAsync(orderId);

            if (Order is null)
                return NotFound("Order not found.");

            return Ok(Order);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet] // GET: /api/orders
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var Orders = await _serviceManager.OrderService.GetAllOrdersAsync();
            
            return Ok(Orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId:guid}")] // PUT: /api/orders/{orderId}
        public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] OrderDto orderDto)
        {
            var Updated = await _serviceManager.OrderService.UpdateOrderAsync(orderId, orderDto);

            if (!Updated)
                return NotFound("Order not found.");

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTO_S;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CustomersController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost] // POST: /api/customers
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            var createdCustomer = await _serviceManager.CustomerService.CreateCustomerAsync(customerDto);
            
            return Ok(createdCustomer);
        }

        [HttpGet("{customerId:Guid}/orders")] // GET: /api/customers/{customerId}/orders
        public async Task<ActionResult> GetCustomerOrders(Guid customerId)
        {
            var Orders = await _serviceManager.OrderService.GetOrderByIdAsync(customerId);
           
            if (Orders is null)
                return NotFound("No orders found for this customer.");

            return Ok(Orders);
        }
    }
}

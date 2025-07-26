using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO_S;

namespace ServiceAbstraction
{
    public interface ICustomerService
    {
        Task<int> CreateCustomerAsync(CustomerDto dto);
        Task<IEnumerable<OrderToReturnDto>> GetCustomerOrdersAsync(int customerId);
    }
}

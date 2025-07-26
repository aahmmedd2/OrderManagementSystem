using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IInvoiceService InvoiceService { get; }
        public IOrderService OrderService { get; }
        public ICustomerService CustomerService { get; }
        public IAuthenticationService AuthenticationService { get; }        
    }
}

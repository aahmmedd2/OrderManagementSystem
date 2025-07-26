using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork,
                                IMapper mapper,
                                IConfiguration configuration) : IServiceManager
    {
        public readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _LazyProductService.Value;

        public readonly Lazy<IInvoiceService> _LazyInvoiceService = new Lazy<IInvoiceService>(() => new InvoiceService(unitOfWork,mapper));
        public IInvoiceService InvoiceService => _LazyInvoiceService.Value;

        public readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(unitOfWork, mapper));
        public IOrderService OrderService => _LazyOrderService.Value;

        public readonly Lazy<ICustomerService> _LazyCustomerService = new Lazy<ICustomerService>(() => new CustomerService(unitOfWork, mapper));
        public ICustomerService CustomerService => _LazyCustomerService.Value;
       
        public readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(unitOfWork, configuration));
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;
    }
}

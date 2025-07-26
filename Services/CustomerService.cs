using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DTO_S;

namespace Services
{
    public class CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : ICustomerService
    {
        public async Task<int> CreateCustomerAsync(CustomerDto dto)
        {
            var Customer = mapper.Map<Customer>(dto);

            var CustomerRepo = unitOfWork.GetRepository<Customer, int>();

            await CustomerRepo.AddAsync(Customer);

            await unitOfWork.SaveChangesAsync();

            return Customer.Id;
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetCustomerOrdersAsync(int customerId)
        {
            var OrderRepo = unitOfWork.GetRepository<Order, Guid>();

            var Orders = await OrderRepo.GetAllAsync();

            return mapper.Map<IEnumerable<OrderToReturnDto>>(Orders);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Enums;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DTO_S;

namespace Services
{
    public class OrderService(IUnitOfWork unitOfWork, IMapper mapper) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto)
        {
            var Customer = await unitOfWork.GetRepository<Customer, int>().GetByIdAsync(orderDto.CustomerId);

            if (Customer is null) throw new Exception("Customer Not Found");

            var Order = new Order()
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = DateTime.Now,
                PaymentMethod = Enum.Parse<PaymentMethod>(orderDto.PaymentMethod),
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>(),
                TotalAmount = 0,
            };

            foreach(var item in orderDto.Items)
            {
                var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.ProductId);

                if (Product is null) throw new Exception($"Product {item.ProductId} not found.");

                var SubTotal = (item.UnitPrice - item.Discount) * item.Quantity;

                Order.TotalAmount += SubTotal;

                Order.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                });
            }
            await unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);

            await unitOfWork.SaveChangesAsync();

            var Result = mapper.Map<OrderToReturnDto>(Order);

            Result.CustomerName = Customer.Name;

            Result.Email = Customer.Email;

            return Result;
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync()
        {
            var Orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync();

            var Result = new List<OrderToReturnDto>();

            foreach(var Order in Orders)
            {
                var OrderDto = mapper.Map<OrderToReturnDto>(Order);

                OrderDto.CustomerName = Order.Customer.Name;
                OrderDto.Email = Order.Customer.Email;
                Result.Add(OrderDto);
            }

            return Result;
        }

        public async Task<OrderToReturnDto?> GetOrderByIdAsync(Guid orderId)
        {
            var OrderRepo = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(orderId);

            if(OrderRepo is null) return null;

            var Order = mapper.Map<OrderToReturnDto>(OrderRepo);

            Order.CustomerName = OrderRepo.Customer.Name;

            Order.Email = OrderRepo.Customer.Email;

            return Order;
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderDto orderDto)
        {
            var ExistingOrder = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(orderId);

            if(ExistingOrder is null) return false;

            ExistingOrder.Status = Enum.TryParse<OrderStatus>(orderDto.Status, out var Stat) ? Stat : ExistingOrder.Status;
            ExistingOrder.PaymentMethod = Enum.TryParse<PaymentMethod>(orderDto.PaymentMethod, out var payment) ? payment : ExistingOrder.PaymentMethod;
            ExistingOrder.TotalAmount = orderDto.TotalAmount;
            ExistingOrder.OrderDate = orderDto.OrderDate;

            if (orderDto.Items != null && orderDto.Items.Any())
            {
                ExistingOrder.OrderItems.Clear();
                foreach (var itemDto in orderDto.Items)
                {
                    ExistingOrder.OrderItems.Add(new OrderItem
                    {
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = itemDto.UnitPrice
                    });
                }
            }

            unitOfWork.GetRepository<Order, Guid>().Update(ExistingOrder);
                        
            await unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using Microsoft.Extensions.Options;
using Shared.DTO_S;

namespace Services.MappingProfiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDto>().ReverseMap();

            CreateMap<Invoice, InvoiceToReturnDto>()
                .ForMember(Dist => Dist.OrderId, Options => Options.MapFrom(Src => Src.Order.Id))
                .ForMember(Dist => Dist.OrderDate, Options => Options.MapFrom(Src => Src.Order.OrderDate))
                .ForMember(Dist => Dist.PaymentMethod, Options => Options.MapFrom(Src => Src.Order.PaymentMethod.ToString()))
                .ForMember(Dist => Dist.OrderStatus, Options => Options.MapFrom(Src => Src.Order.Status.ToString()))
                .ForMember(Dist => Dist.CustomerName, Options => Options.MapFrom(Src => Src.Order.Customer.Name))
                .ForMember(Dist => Dist.Items, Options => Options.MapFrom(Src => Src.Order.OrderItems))
                .ReverseMap();

            CreateMap<OrderItem, InvoiceOrderItemDto>()
                .ForMember(Dist => Dist.ProductName, Options => Options.MapFrom(Src => Src.Product.Name))
                .ReverseMap();
        }
    }
}

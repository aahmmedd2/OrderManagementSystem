using AutoMapper;
using DomainLayer.Models;
using Microsoft.Extensions.Options;
using Shared.DTO_S;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(Dist => Dist.SubTotal, Options =>
                    Options.MapFrom(Src => (Src.UnitPrice * Src.Quantity) - Src.Discount))
                .ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(Dist => Dist.CustomerName, Options => Options.MapFrom(Src => Src.Customer.Name))
                .ForMember(Dist => Dist.Email, Options => Options.MapFrom(Src => Src.Customer.Email))
                .ForMember(Dist => Dist.Status, Options => Options.MapFrom(Src => Src.Status.ToString()))
                .ReverseMap();
        }
    }
}
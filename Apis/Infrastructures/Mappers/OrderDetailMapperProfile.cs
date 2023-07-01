using Application.ViewModels.OrderDetails;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class OrderDetailMapperProfile : Profile
    {
        public OrderDetailMapperProfile()
        {

            CreateMap<OrderDetailRequestDTO, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailResponseDTO, OrderDetail>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.OrderDetailId))
                .ReverseMap();
        }
    }
}

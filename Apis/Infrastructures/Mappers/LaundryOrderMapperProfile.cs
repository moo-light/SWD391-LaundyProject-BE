using Application.ViewModels.LaundryOrders;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class LaundryOrderMapperProfile : Profile
    {
        public LaundryOrderMapperProfile()
        {

            CreateMap<LaundryOrderRequestDTO, LaundryOrder>().ReverseMap();
            CreateMap<LaundryOrderRequestAddDTO, LaundryOrder>()
                        .ForMember(dest => dest.StoreId, src => src.MapFrom(x => x.StoreId))
                        .ReverseMap();
            CreateMap<LaundryOrderResponseDTO, LaundryOrder>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.OrderId))
                .ReverseMap();
        }
    }
}

using Application.Utils;
using Application.ViewModels.LaundryOrders;
using AutoMapper;
using Domain.Entities;
using Application.ViewModels.Stores;

namespace Infrastructures.Mappers
{
    public class StoreMapperProfile : Profile
    {
        public StoreMapperProfile()
        {

            CreateMap<StoreRequestDTO, Store>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.StoreName))
                .ForMember(dest => dest.Address, src => src.MapFrom(x => x.StoreAddress))
                .ReverseMap();
            CreateMap<StoreResponseDTO, Store>()
                .IncludeBase<StoreRequestDTO,Store>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.StoreId))
                .ReverseMap();
        }
    }
}

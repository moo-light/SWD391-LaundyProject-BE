using Application.ViewModels.LaundryOrders;
using Application.ViewModels.Stores;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class ServiceMapperProfile : Profile
    {
        public ServiceMapperProfile()
        {

            CreateMap<ServiceRequestDTO, Service>().ReverseMap();
            CreateMap<ServiceResponseDTO, Service>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.ServiceId))
                .ReverseMap();
        }
    }
}

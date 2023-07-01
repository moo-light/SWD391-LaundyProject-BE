using Application.ViewModels.BatchOfBuildings;
using Application.ViewModels.LaundryOrders;
using Application.ViewModels.BatchOfBuildings;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class SessionMapperProfile : Profile
    {
        public SessionMapperProfile()
        {

            CreateMap<BatchOfBuildingRequestDTO, BatchOfBuilding>().ReverseMap();
            CreateMap<BatchOfBuildingResponseDTO, BatchOfBuilding>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.BatchOfBuildingId))
                .ReverseMap();
        }
    }
}

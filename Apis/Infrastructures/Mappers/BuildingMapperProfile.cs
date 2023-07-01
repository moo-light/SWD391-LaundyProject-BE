using Application.ViewModels.Buildings;
using Application.ViewModels.Feedbacks;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Mappers
{
    public class BuildingMapperProfile : Profile
    {
        public BuildingMapperProfile()
        {
            CreateMap<BuildingRequestDTO, Building>().ReverseMap();
            CreateMap<BuildingResponseDTO, Building>()
                    .ForMember(dest => dest.Id, src => src.MapFrom(x => x.BuildingId))
                    .ReverseMap();
        }
    }
}

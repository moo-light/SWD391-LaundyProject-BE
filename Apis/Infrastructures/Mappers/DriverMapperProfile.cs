using Application.Utils;
using Application.ViewModels.Buildings;
using Application.ViewModels.Customer;
using Application.ViewModels.Drivers;
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
    public class DriverMapperProfile : Profile
    {
        public DriverMapperProfile()
        {
            CreateMap<DriverRegisterDTO, Driver>()
                .ForMember(dest => dest.PasswordHash, src => src.MapFrom(x => x.Password.Hash()))
                .ReverseMap();
            CreateMap<DriverRequestDTO, Driver>()
                .ReverseMap();
            CreateMap<DriverRequestUpdateDTO, Driver>()
                    
                     .ReverseMap();
            CreateMap<DriverResponseDTO, Driver>()
                .ForMember(dest => dest.Batches, src => src.MapFrom(x => x.BatchResponses))
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.DriverId))
                .AfterMap((src, dest, context) =>
                {
                    if (dest.Batches != null)
                    {
                        dest.Batches.Select(x => x.Driver == null);
                    }
                })
                .ReverseMap();
        }
    }
}

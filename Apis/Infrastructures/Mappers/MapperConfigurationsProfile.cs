using AutoMapper;
using Application.Commons;
using Domain.Entities;
using Application.ViewModels.UserViewModels;
using Application.Utils;
using Application.ViewModels.Customer;
using Application.ViewModels.FilterModels;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            CreateMap(typeof(Pagination<>), typeof(List<>)).ReverseMap();

            CreateMap<DriverRegisterDTO, Driver>()
                .ForMember(dest => dest.PasswordHash, src => src.MapFrom(x => x.Password.Hash()))
                .ReverseMap();
            CreateMap<CustomerRegisterDTO, Customer>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(x => x.FullName))
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(x => x.PhoneNumber))
                .ForMember(dest => dest.PasswordHash, src => src.MapFrom(x => x.Password.Hash()))
                .ForMember(dest => dest.Address, src => src.MapFrom(x => x.Address))
                .ReverseMap();
            CreateMap<CustomerFilteringModel, Customer>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(x => x.FullName))
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(x => x.PhoneNumber))
                .ForMember(dest => dest.Address, src => src.MapFrom(x => x.Address))
                .ReverseMap();
            CreateMap<Pagination<Customer>, Pagination<CustomerFilteringModel>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
                .ForMember(dest => dest.TotalItemsCount, opt => opt.MapFrom(src => src.TotalItemsCount))
                .ForMember(dest => dest.TotalPagesCount, opt => opt.MapFrom(src => src.TotalPagesCount))
                .ForMember(dest => dest.Next, opt => opt.MapFrom(src => src.Next))
                .ForMember(dest => dest.Previous, opt => opt.MapFrom(src => src.Previous))
                .ReverseMap();

        }
    }
}

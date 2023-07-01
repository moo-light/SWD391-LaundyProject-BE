using Application.Utils;
using Application.ViewModels.Customer;
using Application.ViewModels.LaundryOrders;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {

            CreateMap<CustomerRegisterDTO, Customer>()
                .ForMember(dest => dest.PasswordHash, src => src.MapFrom(x => x.Password.Hash()))
                .ReverseMap();
            CreateMap<CustomerRequestDTO, Customer>()
                .ReverseMap();
            CreateMap<CustomerRequestUpdateDTO, Customer>()
                    
                     .ReverseMap();
            CreateMap<CustomerResponseDTO, Customer>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.CustomerId))
                .ReverseMap();
        }
    }
}

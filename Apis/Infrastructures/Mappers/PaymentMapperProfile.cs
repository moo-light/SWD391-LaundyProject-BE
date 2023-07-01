using Application.ViewModels.Payments;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class PaymentMapperProfile : Profile
    {
        public PaymentMapperProfile()
        {
            CreateMap<PaymentRequestDTO, Payment>().ReverseMap();
            CreateMap<PaymentResponseDTO, Payment>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.PaymentId))
                .ReverseMap();
        }
    }
}

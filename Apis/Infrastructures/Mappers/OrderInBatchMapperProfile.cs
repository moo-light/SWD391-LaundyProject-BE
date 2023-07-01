using Application.ViewModels.OrderInBatch;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class OrderInBatchMapperProfile : Profile
    {
        public OrderInBatchMapperProfile()
        {

            CreateMap<OrderInBatchRequestDTO, OrderInBatch>().ReverseMap();
            CreateMap<OrderInBatchResponseDTO, OrderInBatch>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.OrderInBatchId))
                .ReverseMap();
        }
    }
}

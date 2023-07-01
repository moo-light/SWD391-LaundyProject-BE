using Application.ViewModels.Feedbacks;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class FeedbackMapperProfile : Profile
    {
        public FeedbackMapperProfile()
        {
            CreateMap<FeedbackRequestDTO, Feedback>().ReverseMap();
            CreateMap<FeedbackResponseDTO, Feedback>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.FeedbackId))
                .ReverseMap();
        }
    }
}

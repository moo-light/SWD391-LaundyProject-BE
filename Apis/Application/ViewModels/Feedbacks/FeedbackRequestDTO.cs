using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Application.ViewModels.Feedbacks
{
    public class FeedbackRequestDTO
    {
        [AllowNull]
        public string? Comment { get; set; }
        public short Rating { get; set; }
        [AllowNull]
        [EnumDataType(typeof(FeedbackStatusEnums))]
        public string? Status { get; set; }
    }
}
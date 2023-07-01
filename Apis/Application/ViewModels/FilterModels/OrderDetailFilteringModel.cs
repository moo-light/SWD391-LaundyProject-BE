using Domain.CustomValidations;
using Domain.Enums;

namespace Application.ViewModels.FilterModels
{
    public class OrderDetailFilteringModel : BaseFilterringModel
    {
        public Guid?[]? OrderId { get; set; }
        public Guid?[]? ServiceId { get; set; }
        public string?[]? Weight { get; set; }
        [EnumValidation(typeof(OrderDetailStatus))]
        public string? Status { get; set; }
    }
}

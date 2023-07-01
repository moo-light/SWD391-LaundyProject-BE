using Domain.CustomValidations;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.OrderDetails
{
    public class OrderDetailRequestDTO
    {
        public Guid? OrderId { get; set; } = null;
        public Guid? ServiceId { get; set; } = null;
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        public decimal Weight { get; set; }
        [EnumValidation(typeof(OrderDetailStatus))]
        public string? Status { get; set; }
    }
}
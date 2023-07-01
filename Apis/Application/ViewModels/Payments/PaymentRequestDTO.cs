using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Payments
{
    public class PaymentRequestDTO
    {
        public Guid?  OrderId { get; set; }
        public decimal? Amount { get; set; }
        [EnumDataType(typeof(PaymentMethodEnum))]
        public string? PaymentMethod { get; set; }
        [EnumDataType(typeof(PaymentStatus))]
        public string? Status { get; set; }
    }
}
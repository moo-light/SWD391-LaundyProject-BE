using Domain.CustomValidations;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.OrderInBatch
{
    public class OrderInBatchRequestDTO
    {
        public Guid? BatchId { get; set; } = null;
        public Guid? OrderId { get; set; } = null;
        [EnumValidation(typeof(OrderInBatchStatus))]
        public string? Status { get; set; }
    }
}
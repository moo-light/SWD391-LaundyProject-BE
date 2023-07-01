using Application.ViewModels.Customer;
using Application.ViewModels.Stores;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Feedbacks
{
    public class FeedbackResponseDTO : FeedbackRequestDTO
    {
        public Guid? FeedbackId { get; set; }
        public virtual CustomerResponseDTO? Customer { get; set; } = null;
        public virtual StoreResponseDTO? Store { get; set; } = null;

    }
}
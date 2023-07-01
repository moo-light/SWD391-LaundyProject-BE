using Application.ViewModels.Feedbacks;
using Application.ViewModels.LaundryOrders;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Stores
{
    public class StoreResponseDTO : StoreRequestDTO
    {
        public Guid? StoreId { get; set; }
        public virtual ICollection<LaundryOrderResponseDTO>? Orders { get; set; } = null;
        public virtual ICollection<ServiceResponseDTO>? Services { get; set; } = null;
        public virtual ICollection<FeedbackResponseDTO>? Feedbacks { get; set; } = null;
    }
}
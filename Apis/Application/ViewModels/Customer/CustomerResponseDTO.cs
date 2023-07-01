using Application.ViewModels.Feedbacks;
using Application.ViewModels.LaundryOrders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Customer
{
    public class CustomerResponseDTO: CustomerRequestDTO
    {
        public Guid? CustomerId { get; set; }
        public ICollection<LaundryOrderResponseDTO> LaundryOrders { get; set; } = null;
        public ICollection<FeedbackResponseDTO> FeedbackResponses { get; set; } = null;
    }
}

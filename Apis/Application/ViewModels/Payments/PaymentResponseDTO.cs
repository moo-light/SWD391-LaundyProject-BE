using Application.ViewModels.LaundryOrders;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Payments
{
    public class PaymentResponseDTO : PaymentRequestDTO
    {
        public Guid? PaymentId { get; set; }
        public virtual LaundryOrderResponseDTO? Order { get; set; } = null;
    }
}

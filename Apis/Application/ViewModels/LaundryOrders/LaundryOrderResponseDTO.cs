using Application.ViewModels.Customer;
using Application.ViewModels.OrderDetails;
using Application.ViewModels.OrderInBatch;
using Application.ViewModels.Payments;
using Application.ViewModels.Stores;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.LaundryOrders
{
    public class LaundryOrderResponseDTO : LaundryOrderRequestDTO
    {
        public Guid? OrderId { get; set; }
        public virtual CustomerResponseDTO? Customer { get; set; } = null;
        public virtual Building? Building { get; set; } = null;
        public virtual StoreResponseDTO? Store { get; set; } = null;
        public virtual ICollection<OrderInBatchResponseDTO> OrderInBatches { get; set; } = null;
        public virtual ICollection<OrderDetailResponseDTO> OrderDetails { get; set; } = null;
        public virtual ICollection<PaymentResponseDTO> Payments { get; set; } = null;
    }
}

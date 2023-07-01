using Application.ViewModels.Batchs;
using Application.ViewModels.LaundryOrders;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.OrderInBatch
{
    public class OrderInBatchResponseDTO : OrderInBatchRequestDTO
    {
        public Guid? OrderInBatchId { get; set; }
        public virtual BatchResponseDTO? Batch { get; set; } = null;
        public virtual LaundryOrderResponseDTO? Order { get; set; } = null;
    }
}

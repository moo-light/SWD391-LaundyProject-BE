using Application.ViewModels.LaundryOrders;
using Application.ViewModels.Stores;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.OrderDetails
{
    public class OrderDetailResponseDTO : OrderDetailRequestDTO
    {
        public Guid? OrderDetailId { get; set; }
        public virtual LaundryOrderResponseDTO? Order { get; set; } = null;// Order status = 
        public virtual ServiceResponseDTO? Service { get; set; } = null;
    }
}

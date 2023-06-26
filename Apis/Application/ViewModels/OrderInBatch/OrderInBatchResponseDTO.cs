﻿using Domain.Entities;
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
        public virtual Batch? Batch { get; set; }
        public virtual LaundryOrder? Order { get; set; }
    }
}

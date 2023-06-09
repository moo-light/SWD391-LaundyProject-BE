﻿using Domain.Entitiess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderDetail:BaseEntity
    {
        public Guid? OrderId { get; set; } = null;
        public Guid? ServiceId { get; set; } = null;
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        public decimal Weight { get; set; }
        public virtual LaundryOrder? Order { get; set; } // Order status = 
        public virtual Service? Service { get; set; } 
    }
}

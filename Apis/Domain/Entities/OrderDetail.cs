using Domain.CustomValidations;
using Domain.Entitiess;
using Domain.Enums;
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
        [EnumValidation(typeof(OrderDetailStatus))]
        public string? Status { get; set; }
        public virtual LaundryOrder? Order { get; set; } // Order status = 
        public virtual Service? Service { get; set; } 
    }
}

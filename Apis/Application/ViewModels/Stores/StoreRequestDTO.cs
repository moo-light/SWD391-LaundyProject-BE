using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Stores
{
    public class StoreRequestDTO
    {
        [Required]
        public string? StoreName { get; set; }
        [Required]
        public string? StoreAddress { get; set; }
        //[Required]
        //public decimal? Price { get; set; } 
    }
}

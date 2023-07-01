using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Customer
{
    public class CustomerRequestDTO
    {
        [StringLength(int.MaxValue,MinimumLength = 5)]
        public string? FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string? Address { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Drivers
{
    public class DriverRegisterDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string? FullName { get; set; }
        [EmailAddress]
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string? Email { get; set; }
        [PasswordPropertyText]
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }
        [Phone]
        [Required]
        public string? PhoneNumber { get; set; }
    }
}

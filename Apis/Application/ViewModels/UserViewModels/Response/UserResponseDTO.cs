using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.UserViewModels.Response
{
    public class UserResponseDTO
    {
        public Guid? Id { get; set; }
        public string? FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [NotMapped]
        public bool? IsAdmin { get; set; }
    }
}

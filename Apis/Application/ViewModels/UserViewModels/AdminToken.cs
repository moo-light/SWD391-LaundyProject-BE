using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.UserViewModels
{
    public class AdminToken
    {
        public Guid Id { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

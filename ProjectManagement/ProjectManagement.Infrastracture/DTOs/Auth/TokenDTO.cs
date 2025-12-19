using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture.DTOs.Auth
{
    public class TokenDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}

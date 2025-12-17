using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture.DTOs.UserDTOs.Queries
{
    public class GetUserResponse
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }

    public class GetUserByIdRequest
    {
        public int UserId { get; set; }
    }
    
    public class GetUserByEmailRequest
    {
        public string Email { get; set; }
    }
}

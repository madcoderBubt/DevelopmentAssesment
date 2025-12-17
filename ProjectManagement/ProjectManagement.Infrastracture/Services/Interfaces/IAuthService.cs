using ProjectManagement.Core.Entities;
using ProjectManagement.Infrastracture.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDTO> GetTokenAsync(string email, string passkey);
    }
}

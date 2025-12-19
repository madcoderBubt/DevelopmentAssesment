using Azure.Core;
using ProjectManagement.CQRS;
using ProjectManagement.Infrastracture.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture.DTOs.Auth
{
    public class TokenHandler(IAuthService _authService) : IQueryHandler<TokenRequest, TokenDTO>
    {
        public async Task<TokenDTO> Handle(TokenRequest request)
        {
            return await _authService.GetTokenAsync(request.Email, request.Password);
        }
    }
}

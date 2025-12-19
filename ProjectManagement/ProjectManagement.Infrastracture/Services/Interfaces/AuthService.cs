using Microsoft.Identity.Client;
using ProjectManagement.Core.Entities;
using ProjectManagement.Infrastracture.DTOs.Auth;
using ProjectManagement.Infrastracture.Interface;
using ProjectManagement.Infrastracture.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture.Services.Interfaces
{
    public class AuthService(JwtTokenService jwtTokenService, IRepositoryAsync<UserEntity> repository) : IAuthService
    {
        //private readonly RepositoryAsync<UserEntity> repositoryAsync;

        async Task<TokenDTO> IAuthService.GetTokenAsync(string email, string passkey)
        {
            var user = (await repository.GetAsync(f => f.Email == email && f.Password == passkey)).FirstOrDefault();

            if (user == null) {
                return null;
            }

            TokenDTO token = new()
            {
                UserName = user.FullName,
                Email = email,
                Token = jwtTokenService.GenerateToken(email, email, user.Role.ToString())
            };

            return token;
        }
    }
}

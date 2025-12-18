using ProjectManagement.Core.Entities;
using ProjectManagement.CQRS;
using ProjectManagement.Infrastracture.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture.DTOs.UserDTOs.Queries
{
    public class GetUserByIdHandler(IRepositoryAsync<UserEntity> _userRepository) : IQueryHandler<GetUserByIdRequest, GetUserResponse>
    {
        public async Task<GetUserResponse> Handle(GetUserByIdRequest query)
        {
            var result = await _userRepository.GetByIdAsync(query.UserId);
            return new() { 
                Email = result.Email, 
                FullName = result.FullName, 
                RoleId = (int) result.Role, 
                Role = result.Role.ToString() 
            };
        }
    }
}

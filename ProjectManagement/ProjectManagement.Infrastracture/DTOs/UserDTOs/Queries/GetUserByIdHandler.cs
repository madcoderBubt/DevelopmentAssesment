using ProjectManagement.Core.Entities;
using ProjectManagement.Infrastracture.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture.DTOs.UserDTOs.Queries
{
    public class GetUserByIdHandler(IRepositoryAsync<UserEntity> _userRepository)
    {
        //private readonly IRepositoryAsync<UserEntity> _userRepository;
        public async Task<GetUserResponse> GetUsers(int Id)
        {
            var user = await _userRepository.GetByIdAsync((long)Id);
            return new() { Email = user.Email, FullName = user.FullName, Role = user.Role.ToString()};
        }
    }
}

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
    public class GetUserAllHandler(IRepositoryAsync<UserEntity> _userRepository)
        : IQueryHandler<GetAllUsersRequest, IList<GetUserResponse>>
    {
        public async Task<IList<GetUserResponse>> Handle(GetAllUsersRequest query)
        {
            var result = await _userRepository.GetAllAsync();
            return result.Aggregate(new List<GetUserResponse>(), (list, user) =>
            {
                list.Add(new GetUserResponse()
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    RoleId = (int)user.Role,
                    Role = user.Role.ToString()
                });
                return list;
            });
        }
    }
}

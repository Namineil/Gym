using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface IUserRoleService
    {
        Task<IEnumerable<User>> ListUserByRoleAsync(int idRole);
        Task<UserResponse> SetRole(int IdUser, int idRole);
    }
}
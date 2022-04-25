using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> ListAsync();
        Task<RoleResponse> SaveAsync(Role role);
        Task<RoleResponse> UpdateAsync(int id, Role role);
        Task<RoleResponse> DeleteAsync(int id);
    }
}
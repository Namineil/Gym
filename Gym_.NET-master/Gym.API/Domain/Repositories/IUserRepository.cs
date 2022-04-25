using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task AddAsync(User user);
        void Update(User user);
        Task<User> FindByIdAsync(int id);
        void Remove(User user);
    }
}
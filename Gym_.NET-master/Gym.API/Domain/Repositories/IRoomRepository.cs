using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> ListAsync();
        Task AddAsync(Room room);
        void Update(Room room);
        Task<Room> FindByIdAsync(int id);
        void Remove(Room room);
    }
}
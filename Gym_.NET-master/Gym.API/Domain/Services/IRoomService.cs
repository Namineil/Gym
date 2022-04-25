using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> ListAsync();
        Task<RoomResponse> SaveAsync(Room room);
        Task<RoomResponse> UpdateAsync(int id, Room room);
        Task<RoomResponse> DeleteAsync(int id);
    }
}
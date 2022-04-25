using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Room room)
        {
            await context.Room.AddAsync(room);
        }

        public async Task<Room> FindByIdAsync(int id)
        {
            return await context.Room.FindAsync(id);
        }

        public async Task<IEnumerable<Room>> ListAsync()
        {
            return await context.Room
                .Include(x => x.AdministratorHall)
                .ThenInclude(x => x.User)
                .ToListAsync();
        }

        public void Remove(Room room)
        {
            context.Room.Remove(room);
        }

        public void Update(Room room)
        {
            context.Room.Update(room);
        }
    }
}
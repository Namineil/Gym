using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class AdministratorHallRepository : BaseRepository, IAdministratorHallRepository
    {
        public AdministratorHallRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(AdministratorHall administratorHall)
        {
            await context.AdministratorHall.AddAsync(administratorHall);
        }

        public async Task<AdministratorHall> FindByIdAsync(int id)
        {
            return await context.AdministratorHall.FindAsync(id);
        }

        public async Task<IEnumerable<AdministratorHall>> ListAsync()
        {
            return await context.AdministratorHall.ToListAsync();
        }

        public void Remove(AdministratorHall administratorHall)
        {
            context.AdministratorHall.Remove(administratorHall);
        }

        public void Update(AdministratorHall administratorHall)
        {
            context.AdministratorHall.Update(administratorHall);
        }
    }
}
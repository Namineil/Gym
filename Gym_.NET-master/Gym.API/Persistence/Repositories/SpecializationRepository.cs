using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class SpecializationRepository : BaseRepository, ISpecializationRepository
    {
        public SpecializationRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Specialization specialization)
        {
            await context.Specialization.AddAsync(specialization);
        }

        public async Task<Specialization> FindByIdAsync(int id)
        {
            return await context.Specialization.FindAsync(id);
        }

        public async Task<IEnumerable<Specialization>> ListAsync()
        {
            return await context.Specialization.ToListAsync();
        }

        public void Remove(Specialization specialization)
        {
            context.Specialization.Remove(specialization);
        }

        public void Update(Specialization specialization)
        {
            context.Specialization.Update(specialization);
        }
    }
}
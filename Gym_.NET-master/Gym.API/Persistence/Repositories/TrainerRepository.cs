using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class TrainerRepository : BaseRepository, ITrainerRepository
    {
        public TrainerRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Trainer trainer)
        {
            await context.Trainer.AddAsync(trainer);
        }

        public async Task<Trainer> FindByIdAsync(int id)
        {
            return await context.Trainer.FindAsync(id);
        }

        public async Task<IEnumerable<Trainer>> ListAsync()
        {
            return await context.Trainer.ToListAsync();
        }

        public void Remove(Trainer trainer)
        {
            context.Trainer.Remove(trainer);
        }

        public void Update(Trainer trainer)
        {
            context.Trainer.Update(trainer);
        }
    }
}
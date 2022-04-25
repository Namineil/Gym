using System.Threading.Tasks;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
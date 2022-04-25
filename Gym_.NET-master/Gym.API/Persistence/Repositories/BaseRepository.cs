using System.Diagnostics;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext context;
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }
    }
}
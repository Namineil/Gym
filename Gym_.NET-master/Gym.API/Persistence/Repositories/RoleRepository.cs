using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Role role)
        {
            await context.Role.AddAsync(role);
        }

        public async Task<Role> FindByIdAsync(int id)
        {
            return await context.Role.FindAsync(id);
        }

        public async Task<IEnumerable<Role>> ListAsync()
        {
            return await context.Role.ToListAsync();
        }

        public void Remove(Role role)
        {
            context.Role.Remove(role);
        }

        public void Update(Role role)
        {
            context.Role.Update(role);
        }
    }
}
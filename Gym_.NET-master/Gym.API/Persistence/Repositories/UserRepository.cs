using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(User user)
        {
            await context.User.AddAsync(user);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await context.User.Include(x => x.UserRoles)
                                        .ThenInclude(x => x.Role)
                                        .FirstOrDefaultAsync(x => x.IdUser == id);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await context.User.Include(x => x.UserRoles)
                                        .ThenInclude(x => x.Role)
                                        .ToListAsync();
        }

        public void Remove(User user)
        {
            context.User.Remove(user);
        }

        public void Update(User user)
        {
            var entity = context.User.Find(user.IdUser);
            if (entity == null) {
                return;
            }
            context.Entry<User>(entity).CurrentValues.SetValues(user);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class ClientRepository : BaseRepository, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Client client)
        {
            await context.Client.AddAsync(client);
        }

        public async Task<Client> FindByIdAsync(int id)
        {
            return await context.Client.FindAsync(id);
        }

         public async Task<Client> FindByIdUserAsync(int idUser)
        {
            return await context.Client.FirstOrDefaultAsync(x => x.IdUser == idUser);
        }

        public async Task<IEnumerable<Client>> ListAsync()
        {
            return await context.Client.ToListAsync();
        }

        public void Remove(Client client)
        {
            context.Client.Remove(client);
        }

        public void Update(Client client)
        {
            context.Client.Update(client);
        }
    }
}
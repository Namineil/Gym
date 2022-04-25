using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> ListAsync();
        Task<Client> FindByIdUserAsync(int idUser);
        Task AddAsync(Client client);
        void Update(Client client);
        Task<Client> FindByIdAsync(int id);
        void Remove(Client client);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> ListAsync();
        Task<Client> GetClientAsync(int idUser);
        Task<ClientResponse> SaveAsync(Client client);
        Task<ClientResponse> UpdateAsync(int id, Client client);
        Task<ClientResponse> DeleteAsync(int id);
    }
}
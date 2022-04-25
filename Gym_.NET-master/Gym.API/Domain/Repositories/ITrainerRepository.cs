using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface ITrainerRepository
    {
        Task<IEnumerable<Trainer>> ListAsync();
        Task AddAsync(Trainer trainer);
        void Update(Trainer trainer);
        Task<Trainer> FindByIdAsync(int id);
        void Remove(Trainer trainer);
    }
}
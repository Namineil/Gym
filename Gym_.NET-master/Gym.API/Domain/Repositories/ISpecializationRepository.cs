using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface ISpecializationRepository
    {
        Task<IEnumerable<Specialization>> ListAsync();
        Task AddAsync(Specialization specialization);
        void Update(Specialization specialization);
        Task<Specialization> FindByIdAsync(int id);
        void Remove(Specialization specialization);
    }
}
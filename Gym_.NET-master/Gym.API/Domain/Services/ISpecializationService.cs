using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface ISpecializationService
    {
        Task<IEnumerable<Specialization>> ListAsync();
        Task<SpecializationResponse> SaveAsync(Specialization specialization);
        Task<SpecializationResponse> UpdateAsync(int id, Specialization specialization);
        Task<SpecializationResponse> DeleteAsync(int id);
    }
}
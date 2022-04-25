using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface ITrainerService
    {
        Task<IEnumerable<Trainer>> ListAsync();
        Task<TrainerResponse> SaveAsync(Trainer trainer);
        Task<TrainerResponse> UpdateAsync(int id, Trainer trainer);
        Task<TrainerResponse> DeleteAsync(int id);
    }
}
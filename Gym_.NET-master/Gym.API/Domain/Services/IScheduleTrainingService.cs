using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface IScheduleTrainingService
    {
        Task<IEnumerable<ScheduleTraining>> ListAsync();
        Task<ScheduleTraining> FindByIdAsync(int id);
        Task<IEnumerable<ScheduleTraining>> ListIdSpecializationAsync(int idSpecialization);

        Task<ScheduleTrainingResponse> SaveAsync(ScheduleTraining scheduleTraining);
        Task<ScheduleTrainingResponse> UpdateAsync(int id, ScheduleTraining scheduleTraining);
        Task<ScheduleTrainingResponse> DeleteAsync(int id);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface IScheduleTrainingRepository
    {
        Task<IEnumerable<ScheduleTraining>> ListAsync();
        Task<IEnumerable<ScheduleTraining>> ListIdSpecializationAsync(int idSpecialization);
        Task AddAsync(ScheduleTraining scheduleTraining);
        void Update(ScheduleTraining scheduleTraining);
        Task<ScheduleTraining> FindByIdAsync(int id);
        void Remove(ScheduleTraining scheduleTraining);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class ScheduleTrainingService : IScheduleTrainingService
    {
        private readonly IScheduleTrainingRepository scheduleTrainingRepository;
        private readonly IUnitOfWork unitOfWork;
        public ScheduleTrainingService(IScheduleTrainingRepository scheduleTrainingRepository, IUnitOfWork unitOfWork)
        {
            this.scheduleTrainingRepository = scheduleTrainingRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ScheduleTrainingResponse> DeleteAsync(int id)
        {
            var existingScheduleTraining = await scheduleTrainingRepository.FindByIdAsync(id);
            if (existingScheduleTraining == null)
                return new ScheduleTrainingResponse("Расписание занятия не найдено!");
            try
            {
                scheduleTrainingRepository.Remove(existingScheduleTraining);
                await unitOfWork.CompleteAsync();

                return new ScheduleTrainingResponse(existingScheduleTraining);
            }
            catch (Exception ex)
            {
                return new ScheduleTrainingResponse($"Ошибка при удалении Расписание занятия: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ScheduleTraining>> ListAsync()
        {
            return await scheduleTrainingRepository.ListAsync();
        }

        public async Task<ScheduleTraining> FindByIdAsync(int id)
        {
            return await scheduleTrainingRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<ScheduleTraining>> ListIdSpecializationAsync(int idSpecialization)
        {
            return await scheduleTrainingRepository.ListIdSpecializationAsync(idSpecialization);
        }

        public async Task<ScheduleTrainingResponse> SaveAsync(ScheduleTraining scheduleTraining)
        {
            try
            {
                await scheduleTrainingRepository.AddAsync(scheduleTraining);
                await unitOfWork.CompleteAsync();

                return new ScheduleTrainingResponse(scheduleTraining);
            }
            catch (Exception ex)
            {
                return new ScheduleTrainingResponse($"Ошибка при сохранении Расписание занятия: {ex.Message}");
            }
        }

        public async Task<ScheduleTrainingResponse> UpdateAsync(int id, ScheduleTraining scheduleTraining)
        {
            var existingScheduleTraining = await scheduleTrainingRepository.FindByIdAsync(id);
            if (existingScheduleTraining == null)
                return new ScheduleTrainingResponse("Расписание занятия не найдено!");
            
            existingScheduleTraining.IdTraining = scheduleTraining.IdTraining;

            try
            {
                scheduleTrainingRepository.Update(existingScheduleTraining);
                await unitOfWork.CompleteAsync();

                return new ScheduleTrainingResponse(existingScheduleTraining);
            }
            catch (Exception ex)
            {
                return new ScheduleTrainingResponse($"Ошибка при обновлении Расписание занятия: {ex.Message}");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository trainerRepository;
        private readonly IUnitOfWork unitOfWork;
        public TrainerService(ITrainerRepository trainerRepository, IUnitOfWork unitOfWork)
        {
            this.trainerRepository = trainerRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<TrainerResponse> DeleteAsync(int id)
        {
            var existingTrainer = await trainerRepository.FindByIdAsync(id);
            if (existingTrainer == null)
                return new TrainerResponse("Тренера не найдено!");
            try
            {
                trainerRepository.Remove(existingTrainer);
                await unitOfWork.CompleteAsync();

                return new TrainerResponse(existingTrainer);
            }
            catch (Exception ex)
            {
                return new TrainerResponse($"Ошибка при удалении Тренера: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Trainer>> ListAsync()
        {
            return await trainerRepository.ListAsync();
        }

        public async Task<TrainerResponse> SaveAsync(Trainer trainer)
        {
            try
            {
                await trainerRepository.AddAsync(trainer);
                await unitOfWork.CompleteAsync();

                return new TrainerResponse(trainer);
            }
            catch (Exception ex)
            {
                return new TrainerResponse($"Ошибка при сохранении Тренера: {ex.Message}");
            }
        }

        public async Task<TrainerResponse> UpdateAsync(int id, Trainer trainer)
        {
            var existingTrainer = await trainerRepository.FindByIdAsync(id);
            if (existingTrainer == null)
                return new TrainerResponse("Тренера не найдено!");
            
            existingTrainer.User.FullName = trainer.User.FullName;

            try
            {
                trainerRepository.Update(existingTrainer);
                await unitOfWork.CompleteAsync();

                return new TrainerResponse(existingTrainer);
            }
            catch (Exception ex)
            {
                return new TrainerResponse($"Ошибка при обновлении Тренера: {ex.Message}");
            }
        }
    }
}
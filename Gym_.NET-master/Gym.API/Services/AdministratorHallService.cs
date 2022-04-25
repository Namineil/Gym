using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class AdministratorHallService : IAdministratorHallService
    {
        private readonly IAdministratorHallRepository administratorHallRepository;
        private readonly IUnitOfWork unitOfWork;
        public AdministratorHallService(IAdministratorHallRepository administratorHallRepository, IUnitOfWork unitOfWork)
        {
            this.administratorHallRepository = administratorHallRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<AdministratorHallResponse> DeleteAsync(int id)
        {
            var existingAdministratorHall = await administratorHallRepository.FindByIdAsync(id);
            if (existingAdministratorHall == null)
                return new AdministratorHallResponse("Администратора не найдено!");
            try
            {
                administratorHallRepository.Remove(existingAdministratorHall);
                await unitOfWork.CompleteAsync();

                return new AdministratorHallResponse(existingAdministratorHall);
            }
            catch (Exception ex)
            {
                return new AdministratorHallResponse($"Ошибка при удалении Администратора: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AdministratorHall>> ListAsync()
        {
            return await administratorHallRepository.ListAsync();
        }

        public async Task<AdministratorHallResponse> SaveAsync(AdministratorHall administratorHall)
        {
            try
            {
                await administratorHallRepository.AddAsync(administratorHall);
                await unitOfWork.CompleteAsync();

                return new AdministratorHallResponse(administratorHall);
            }
            catch (Exception ex)
            {
                return new AdministratorHallResponse($"Ошибка при сохранении Администратора: {ex.Message}");
            }
        }

        public async Task<AdministratorHallResponse> UpdateAsync(int id, AdministratorHall administratorHall)
        {
            var existingAdministratorHall = await administratorHallRepository.FindByIdAsync(id);
            if (existingAdministratorHall == null)
                return new AdministratorHallResponse("Администратора не найдено!");
            
            existingAdministratorHall.User.FullName = administratorHall.User.FullName;

            try
            {
                administratorHallRepository.Update(existingAdministratorHall);
                await unitOfWork.CompleteAsync();

                return new AdministratorHallResponse(existingAdministratorHall);
            }
            catch (Exception ex)
            {
                return new AdministratorHallResponse($"Ошибка при обновлении Администратора: {ex.Message}");
            }
        }
    }
}
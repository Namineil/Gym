using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class ServicesCardService : IServicesCardService
    {
        private readonly IServicesCardRepository servicesServicesCardRepository;
        private readonly IUnitOfWork unitOfWork;
        public ServicesCardService(IServicesCardRepository servicesServicesCardRepository, IUnitOfWork unitOfWork)
        {
            this.servicesServicesCardRepository = servicesServicesCardRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ServicesCardResponse> DeleteAsync(int id)
        {
            var existingServicesCard = await servicesServicesCardRepository.FindByIdAsync(id);
            if (existingServicesCard == null)
                return new ServicesCardResponse("Услуга не найдена!");
            try
            {
                servicesServicesCardRepository.Remove(existingServicesCard);
                await unitOfWork.CompleteAsync();

                return new ServicesCardResponse(existingServicesCard);
            }
            catch (Exception ex)
            {
                return new ServicesCardResponse($"Ошибка при удалении Услуги: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ServicesCard>> FindByIdCardListAsync(int idCard)
        {
            return await servicesServicesCardRepository.FindByIdCardListAsync(idCard);
        }

        public async Task<ServicesCardResponse> SaveAsync(ServicesCard servicesServicesCard)
        {
            try
            {
                await servicesServicesCardRepository.AddAsync(servicesServicesCard);
                await unitOfWork.CompleteAsync();

                return new ServicesCardResponse(servicesServicesCard);
            }
            catch (Exception ex)
            {
                return new ServicesCardResponse($"Ошибка при сохранении Услуги: {ex.Message}");
            }
        }

        public async Task<ServicesCardResponse> UpdateAsync(int id, ServicesCard servicesServicesCard)
        {
            var existingServicesCard = await servicesServicesCardRepository.FindByIdAsync(id);
            if (existingServicesCard == null)
                return new ServicesCardResponse("Услуга не найдена!");
            
            existingServicesCard.Name = servicesServicesCard.Name;

            try
            {
                servicesServicesCardRepository.Update(existingServicesCard);
                await unitOfWork.CompleteAsync();

                return new ServicesCardResponse(existingServicesCard);
            }
            catch (Exception ex)
            {
                return new ServicesCardResponse($"Ошибка при обновлении Услуги: {ex.Message}");
            }
        }
    }
}
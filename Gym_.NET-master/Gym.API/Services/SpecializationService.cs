using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository specializationRepository;
        private readonly IUnitOfWork unitOfWork;
        public SpecializationService(ISpecializationRepository specializationRepository, IUnitOfWork unitOfWork)
        {
            this.specializationRepository = specializationRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<SpecializationResponse> DeleteAsync(int id)
        {
            var existingSpecialization = await specializationRepository.FindByIdAsync(id);
            if (existingSpecialization == null)
                return new SpecializationResponse("Специализация не найдена!");
            try
            {
                specializationRepository.Remove(existingSpecialization);
                await unitOfWork.CompleteAsync();

                return new SpecializationResponse(existingSpecialization);
            }
            catch (Exception ex)
            {
                return new SpecializationResponse($"Ошибка при удалении Специализации: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Specialization>> ListAsync()
        {
            return await specializationRepository.ListAsync();
        }

        public async Task<SpecializationResponse> SaveAsync(Specialization specialization)
        {
            try
            {
                await specializationRepository.AddAsync(specialization);
                await unitOfWork.CompleteAsync();

                return new SpecializationResponse(specialization);
            }
            catch (Exception ex)
            {
                return new SpecializationResponse($"Ошибка при сохранении Специализации: {ex.Message}");
            }
        }

        public async Task<SpecializationResponse> UpdateAsync(int id, Specialization specialization)
        {
            var existingSpecialization = await specializationRepository.FindByIdAsync(id);
            if (existingSpecialization == null)
                return new SpecializationResponse("Специализация не найдена!");
            
            existingSpecialization.Name = specialization.Name;

            try
            {
                specializationRepository.Update(existingSpecialization);
                await unitOfWork.CompleteAsync();

                return new SpecializationResponse(existingSpecialization);
            }
            catch (Exception ex)
            {
                return new SpecializationResponse($"Ошибка при обновлении Специализации: {ex.Message}");
            }
        }
    }
}
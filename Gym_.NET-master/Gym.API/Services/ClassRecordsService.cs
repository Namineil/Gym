using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class ClassRecordsService : IClassRecordsService
    {
        private readonly IClassRecordsRepository classRecordsRepository;
        private readonly IUnitOfWork unitOfWork;
        public ClassRecordsService(IClassRecordsRepository classRecordsRepository, IUnitOfWork unitOfWork)
        {
            this.classRecordsRepository = classRecordsRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ClassRecordsResponse> DeleteAsync(int id)
        {
            var existingClassRecords = await classRecordsRepository.FindByIdAsync(id);
            if (existingClassRecords == null)
                return new ClassRecordsResponse("Запись занятий не найдено!");
            try
            {
                classRecordsRepository.Remove(existingClassRecords);
                await unitOfWork.CompleteAsync();

                return new ClassRecordsResponse(existingClassRecords);
            }
            catch (Exception ex)
            {
                return new ClassRecordsResponse($"Ошибка при удалении Записи занятий: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ClassRecords>> ListAsync()
        {
            return await classRecordsRepository.ListAsync();
        }

        public async Task<ClassRecords> FindByIdAsync(int id)
        {
            return await classRecordsRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<ClassRecords>> GetClassRecordsClientListAsync(int IdClient)
        {
            return await classRecordsRepository.GetClassRecordsClientListAsync(IdClient);
        }

        public async Task<ClassRecordsResponse> SaveAsync(ClassRecords classRecords)
        {
            try
            {
                await classRecordsRepository.AddAsync(classRecords);
                await unitOfWork.CompleteAsync();

                return new ClassRecordsResponse(classRecords);
            }
            catch (Exception ex)
            {
                return new ClassRecordsResponse($"Ошибка при сохранении Записи занятий: {ex.Message}");
            }
        }

        public async Task<ClassRecordsResponse> UpdateAsync(int id, ClassRecords classRecords)
        {
            var existingClassRecords = await classRecordsRepository.FindByIdAsync(id);
            if (existingClassRecords == null)
                return new ClassRecordsResponse("Запись занятий не найдено!");
            
            existingClassRecords.IdClassRecords = classRecords.IdClassRecords;

            try
            {
                classRecordsRepository.Update(existingClassRecords);
                await unitOfWork.CompleteAsync();

                return new ClassRecordsResponse(existingClassRecords);
            }
            catch (Exception ex)
            {
                return new ClassRecordsResponse($"Ошибка при обновлении Записи занятий: {ex.Message}");
            }
        }
    }
}
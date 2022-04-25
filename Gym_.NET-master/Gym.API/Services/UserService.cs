using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await userRepository.FindByIdAsync(id);
            if (existingUser == null)
                return new UserResponse("Пользователя не найдено!");
            try
            {
                userRepository.Remove(existingUser);
                await unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Ошибка при удалении Пользователя: {ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await userRepository.ListAsync();
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                await userRepository.AddAsync(user);
                await unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Ошибка при сохранении Пользователя: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int id, User user)
        {
            var existingUser = await userRepository.FindByIdAsync(id);
            if (existingUser == null)
                return new UserResponse("Пользователя не найдено!");

            user.IdUser = id;
        
            try
            {
                userRepository.Update(user);
                await unitOfWork.CompleteAsync();
                
                existingUser = await userRepository.FindByIdAsync(id);
                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Ошибка при обновлении Пользователя: {ex.Message}");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUnitOfWork unitOfWork;
        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            this.roleRepository = roleRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<RoleResponse> DeleteAsync(int id)
        {
            var role = await roleRepository.FindByIdAsync(id);
            if (role == null)
                return new RoleResponse("Роль не найдено!");
            try
            {
                roleRepository.Remove(role);
                await unitOfWork.CompleteAsync();

                return new RoleResponse(role);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Ошибка при удалении Роли: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Role>> ListAsync()
        {
            return await roleRepository.ListAsync();
        }

        public async Task<RoleResponse> SaveAsync(Role role)
        {
            try
            {
                await roleRepository.AddAsync(role);
                await unitOfWork.CompleteAsync();

                return new RoleResponse(role);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Ошибка при сохранении Роли: {ex.Message}");
            }
        }

        public async Task<RoleResponse> UpdateAsync(int id, Role role)
        {
            var findRole = await roleRepository.FindByIdAsync(id);
            if (findRole == null)
                return new RoleResponse("Роль не найдено!");
            
            findRole.Name = role.Name;

            try
            {
                roleRepository.Update(findRole);
                await unitOfWork.CompleteAsync();

                return new RoleResponse(findRole);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Ошибка при обновлении Роли: {ex.Message}");
            }
        }
    }
}
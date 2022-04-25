using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        public UserRoleService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> ListUserByRoleAsync(int idRole)
        {
            var users = await userRepository.ListAsync();
            var usersInRole = users.Where(x => x.UserRoles.Contains(x.UserRoles.SingleOrDefault(y => y.IdRole == idRole)));
            return usersInRole;
        }

        public async Task<UserResponse> SetRole(int IdUser, int idRole)
        {
            try
            {
                var user = await userRepository.FindByIdAsync(IdUser);
                user.UserRoles.Add(new UserRole {
                    IdUser = IdUser,
                    IdRole = idRole
                });
                await unitOfWork.CompleteAsync();
                user = await userRepository.FindByIdAsync(IdUser);
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Ошибка при установке роли: {ex.Message}");
            }
        }
    }
}
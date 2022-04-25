using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface IAdministratorHallService
    {
        Task<IEnumerable<AdministratorHall>> ListAsync();
        Task<AdministratorHallResponse> SaveAsync(AdministratorHall administratorHall);
        Task<AdministratorHallResponse> UpdateAsync(int id, AdministratorHall administratorHall);
        Task<AdministratorHallResponse> DeleteAsync(int id);
    }
}
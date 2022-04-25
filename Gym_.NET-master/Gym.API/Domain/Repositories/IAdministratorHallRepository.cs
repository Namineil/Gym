using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface IAdministratorHallRepository
    {
        Task<IEnumerable<AdministratorHall>> ListAsync();
        Task AddAsync(AdministratorHall administratorHall);
        void Update(AdministratorHall administratorHall);
        Task<AdministratorHall> FindByIdAsync(int id);
        void Remove(AdministratorHall administratorHall);
    }
}
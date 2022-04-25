using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface IServicesCardRepository
    {
        Task<IEnumerable<ServicesCard>> FindByIdCardListAsync(int idCard);
        Task AddAsync(ServicesCard servicesCard);
        void Update(ServicesCard servicesCard);
        Task<ServicesCard> FindByIdAsync(int id);
        void Remove(ServicesCard servicesCard);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface IServicesCardService
    {
        Task<IEnumerable<ServicesCard>> FindByIdCardListAsync(int idCard);
        Task<ServicesCardResponse> SaveAsync(ServicesCard servicesCard);
        Task<ServicesCardResponse> UpdateAsync(int id, ServicesCard servicesCard);
        Task<ServicesCardResponse> DeleteAsync(int id);
    }
}
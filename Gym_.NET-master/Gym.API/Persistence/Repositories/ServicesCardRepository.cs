using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class ServicesCardRepository : BaseRepository, IServicesCardRepository
    {
        public ServicesCardRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(ServicesCard servicesServicesCard)
        {
            await context.ServicesCard.AddAsync(servicesServicesCard);
        }

        public async Task<ServicesCard> FindByIdAsync(int id)
        {
            return await context.ServicesCard.FindAsync(id);
        }

        public async Task<IEnumerable<ServicesCard>> FindByIdCardListAsync(int idCard)
        {
            return await context.ServicesCard.Where(it=>it.IdCard == idCard).ToListAsync();
        }

        public void Remove(ServicesCard servicesServicesCard)
        {
            context.ServicesCard.Remove(servicesServicesCard);
        }

        public void Update(ServicesCard servicesServicesCard)
        {
            context.ServicesCard.Update(servicesServicesCard);
        }
    }
}
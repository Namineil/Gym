using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class CardSalesHistoryRepository : BaseRepository, ICardSalesHistoryRepository
    {
        public CardSalesHistoryRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(CardSalesHistory cardSalesHistory)
        {
            await context.CardSalesHistory.AddAsync(cardSalesHistory);
        }

        public async Task<CardSalesHistory> FindByIdAsync(int id)
        {
            return await context.CardSalesHistory.FindAsync(id);
        }

        public async Task<CardSalesHistory> FindByIdUserAsync(int idUser)
        {
            return await context.CardSalesHistory.Where(it=>it.IdUser == idUser).OrderByDescending(t=>t.Date).FirstOrDefaultAsync();
        }

        public void Remove(CardSalesHistory cardSalesHistory)
        {
            context.CardSalesHistory.Remove(cardSalesHistory);
        }

        public void Update(CardSalesHistory cardSalesHistory)
        {
            context.CardSalesHistory.Update(cardSalesHistory);
        }
    }
}
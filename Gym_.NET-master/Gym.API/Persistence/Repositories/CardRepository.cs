using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class CardRepository : BaseRepository, ICardRepository
    {
        public CardRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Card card)
        {
            await context.Card.AddAsync(card);
        }

        public async Task<Card> FindByIdAsync(int id)
        {
            return await context.Card.FindAsync(id);
        }

        public async Task<IEnumerable<Card>> ListAsync()
        {
            return await context.Card.ToListAsync();
        }

        public void Remove(Card card)
        {
            context.Card.Remove(card);
        }

        public void Update(Card card)
        {
            context.Card.Update(card);
        }
    }
}
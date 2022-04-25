using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> ListAsync();
        Task AddAsync(Card card);
        void Update(Card card);
        Task<Card> FindByIdAsync(int id);
        void Remove(Card card);
    }
}
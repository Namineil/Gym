using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> ListAsync();
        Task<Card> CardAsync(int id);
        Task<CardResponse> SaveAsync(Card card);
        Task<CardResponse> UpdateAsync(int id, Card card);
        Task<CardResponse> DeleteAsync(int id);
    }
}
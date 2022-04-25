using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface ICardSalesHistoryRepository
    {
        Task<CardSalesHistory> FindByIdUserAsync(int idUser);
        Task AddAsync(CardSalesHistory cardSalesHistory);
        void Update(CardSalesHistory cardSalesHistory);
        Task<CardSalesHistory> FindByIdAsync(int id);
        void Remove(CardSalesHistory cardSalesHistory);
    }
}
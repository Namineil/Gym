using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface ICardSalesHistoryService
    {
        Task<CardSalesHistory> FindByIdUserAsync(int idUser);
        Task<CardSalesHistory> CardSalesHistoryAsync(int id);
        Task<CardSalesHistoryResponse> SaveAsync(CardSalesHistory cardSalesHistory);
        Task<CardSalesHistoryResponse> UpdateAsync(int id, CardSalesHistory cardSalesHistory);
        Task<CardSalesHistoryResponse> DeleteAsync(int id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class CardSalesHistoryService : ICardSalesHistoryService
    {
        private readonly ICardSalesHistoryRepository cardSalesHistoryRepository;
        private readonly IUnitOfWork unitOfWork;
        public CardSalesHistoryService(ICardSalesHistoryRepository cardSalesHistoryRepository, IUnitOfWork unitOfWork)
        {
            this.cardSalesHistoryRepository = cardSalesHistoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CardSalesHistory> CardSalesHistoryAsync(int id) 
        {
            var cardSalesHistory = await cardSalesHistoryRepository.FindByIdAsync(id);
            return cardSalesHistory;
        }

        public async Task<CardSalesHistoryResponse> DeleteAsync(int id)
        {
            var existingCardSalesHistory = await cardSalesHistoryRepository.FindByIdAsync(id);
            if (existingCardSalesHistory == null)
                return new CardSalesHistoryResponse("Продажу не найдено!");
            try
            {
                cardSalesHistoryRepository.Remove(existingCardSalesHistory);
                await unitOfWork.CompleteAsync();

                return new CardSalesHistoryResponse(existingCardSalesHistory);
            }
            catch (Exception ex)
            {
                return new CardSalesHistoryResponse($"Ошибка при удалении продажи: {ex.Message}");
            }
        }

        public async Task<CardSalesHistory> FindByIdUserAsync(int idUser)
        {
            return await cardSalesHistoryRepository.FindByIdUserAsync(idUser);
        }

        public async Task<CardSalesHistoryResponse> SaveAsync(CardSalesHistory cardSalesHistory)
        {
            try
            {
                await cardSalesHistoryRepository.AddAsync(cardSalesHistory);
                await unitOfWork.CompleteAsync();

                return new CardSalesHistoryResponse(cardSalesHistory);
            }
            catch (Exception ex)
            {
                return new CardSalesHistoryResponse($"Ошибка при сохранении продажи: {ex.Message}");
            }
        }

        public async Task<CardSalesHistoryResponse> UpdateAsync(int id, CardSalesHistory cardSalesHistory)
        {
            var existingCardSalesHistory = await cardSalesHistoryRepository.FindByIdAsync(id);
            if (existingCardSalesHistory == null)
                return new CardSalesHistoryResponse("Продажи не найдено!");
            
            existingCardSalesHistory.IdCard = cardSalesHistory.IdCard;
            existingCardSalesHistory.IdUser = cardSalesHistory.IdUser;

            try
            {
                cardSalesHistoryRepository.Update(existingCardSalesHistory);
                await unitOfWork.CompleteAsync();

                return new CardSalesHistoryResponse(existingCardSalesHistory);
            }
            catch (Exception ex)
            {
                return new CardSalesHistoryResponse($"Ошибка при обновлении продаж: {ex.Message}");
            }
        }
    }
}
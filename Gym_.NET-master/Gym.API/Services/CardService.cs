using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository cardRepository;
        private readonly IUnitOfWork unitOfWork;
        public CardService(ICardRepository cardRepository, IUnitOfWork unitOfWork)
        {
            this.cardRepository = cardRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Card> CardAsync(int id) 
        {
            var card = await cardRepository.FindByIdAsync(id);
            return card;
        }

        public async Task<CardResponse> DeleteAsync(int id)
        {
            var existingCard = await cardRepository.FindByIdAsync(id);
            if (existingCard == null)
                return new CardResponse("Карту не найдено!");
            try
            {
                cardRepository.Remove(existingCard);
                await unitOfWork.CompleteAsync();

                return new CardResponse(existingCard);
            }
            catch (Exception ex)
            {
                return new CardResponse($"Ошибка при удалении Карты: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Card>> ListAsync()
        {
            return await cardRepository.ListAsync();
        }

        public async Task<CardResponse> SaveAsync(Card card)
        {
            try
            {
                await cardRepository.AddAsync(card);
                await unitOfWork.CompleteAsync();

                return new CardResponse(card);
            }
            catch (Exception ex)
            {
                return new CardResponse($"Ошибка при сохранении Карты: {ex.Message}");
            }
        }

        public async Task<CardResponse> UpdateAsync(int id, Card card)
        {
            var existingCard = await cardRepository.FindByIdAsync(id);
            if (existingCard == null)
                return new CardResponse("Карту не найдено!");
            
            existingCard.Name = card.Name;

            try
            {
                cardRepository.Update(existingCard);
                await unitOfWork.CompleteAsync();

                return new CardResponse(existingCard);
            }
            catch (Exception ex)
            {
                return new CardResponse($"Ошибка при обновлении Карты: {ex.Message}");
            }
        }
    }
}
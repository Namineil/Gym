using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class CardResponse : BaseResponse
    {
        public Card Card {get; private set;}
        public CardResponse(bool success, string message, Card card) : base(success, message)
        {
            Card = card;
        }

        public CardResponse(Card card): this(true, string.Empty, card){}

        public CardResponse(string message): this(false, message, null) {}
       
    }
}
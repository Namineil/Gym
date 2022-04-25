using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class CardSalesHistoryResponse : BaseResponse
    {
        public CardSalesHistory CardSalesHistory {get; private set;}
        public CardSalesHistoryResponse(bool success, string message, CardSalesHistory cardSalesHistory) : base(success, message)
        {
            CardSalesHistory = cardSalesHistory;
        }

        public CardSalesHistoryResponse(CardSalesHistory cardSalesHistory): this(true, string.Empty, cardSalesHistory){}

        public CardSalesHistoryResponse(string message): this(false, message, null) {}
       
    }
}
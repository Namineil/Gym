using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class ServicesCardResponse : BaseResponse
    {
        public ServicesCard ServicesCard {get; private set;}
        public ServicesCardResponse(bool success, string message, ServicesCard servicesCard) : base(success, message)
        {
            ServicesCard = servicesCard;
        }

        public ServicesCardResponse(ServicesCard servicesCard): this(true, string.Empty, servicesCard){}

        public ServicesCardResponse(string message): this(false, message, null) {}
       
    }
}
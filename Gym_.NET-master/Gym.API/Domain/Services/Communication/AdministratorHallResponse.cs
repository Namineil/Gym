using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class AdministratorHallResponse : BaseResponse
    {
        public AdministratorHall AdministratorHall {get; private set;}
        public AdministratorHallResponse(bool success, string message, AdministratorHall administratorHall) : base(success, message)
        {
            AdministratorHall = administratorHall;
        }

        public AdministratorHallResponse(AdministratorHall administratorHall): this(true, string.Empty, administratorHall){}

        public AdministratorHallResponse(string message): this(false, message, null) {}
       
    }
}
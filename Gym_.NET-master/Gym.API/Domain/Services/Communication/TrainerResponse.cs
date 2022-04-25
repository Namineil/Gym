using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class TrainerResponse : BaseResponse
    {
        public Trainer Trainer {get; private set;}
        public TrainerResponse(bool success, string message, Trainer trainer) : base(success, message)
        {
            Trainer = trainer;
        }

        public TrainerResponse(Trainer trainer): this(true, string.Empty, trainer){}

        public TrainerResponse(string message): this(false, message, null) {}
       
    }
}
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class SpecializationResponse : BaseResponse
    {
        public Specialization Specialization {get; private set;}
        public SpecializationResponse(bool success, string message, Specialization specialization) : base(success, message)
        {
            Specialization = specialization;
        }

        public SpecializationResponse(Specialization specialization): this(true, string.Empty, specialization){}

        public SpecializationResponse(string message): this(false, message, null) {}
       
    }
}
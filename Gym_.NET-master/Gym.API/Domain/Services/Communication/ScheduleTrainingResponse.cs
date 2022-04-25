using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class ScheduleTrainingResponse : BaseResponse
    {
        public ScheduleTraining ScheduleTraining {get; private set;}
        public ScheduleTrainingResponse(bool success, string message, ScheduleTraining scheduleTraining) : base(success, message)
        {
            ScheduleTraining = scheduleTraining;
        }

        public ScheduleTrainingResponse(ScheduleTraining scheduleTraining): this(true, string.Empty, scheduleTraining){}

        public ScheduleTrainingResponse(string message): this(false, message, null) {}
       
    }
}
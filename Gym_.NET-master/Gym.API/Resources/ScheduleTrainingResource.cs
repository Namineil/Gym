using System;
using System.Collections.Generic;

namespace Gym.API.Resources
{

    public class ScheduleTrainingResource
    {
        public int IdTraining { get; set; }

        public int IdTrainer { get; set; }
        public string TrainerUserFullName { get; set; }
        public string TrainerUserImage { get; set; }
        public int IdRoom { get; set; }
        public string RoomName { get; set; }

        public DateTimeOffset TrainingDateFrom { get; set; }

        public DateTimeOffset TrainingDateTo { get; set; }
        public int IdSpecialization { get; set; }
        public string SpecializationName { get; set; }

        public string Type { get; set; }

        public int Roominess { get; set; }

        public int Engaged { get; set; }

        public bool RecordIsClosed { get; set; }
    }

}
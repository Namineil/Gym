using System;

namespace Gym.API.Resources
{

    public class ClassRecordsResource
    {
        public int IdClassRecords { get; set; }
        public int IdClient { get; set; }
        public string ClientUserFullName { get; set; }

        public int IdTraining { get; set; }
        public DateTimeOffset ScheduleTrainingTrainingDateFrom { get; set; }
        
        public bool? Presence {get;set;}
    }
}
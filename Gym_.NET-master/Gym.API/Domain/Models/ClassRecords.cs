using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.API.Domain.Models
{

    public class ClassRecords
    {
        public int IdClassRecords { get; set; }

        public int IdTraining { get; set; }

        public int IdClient { get; set; }
        public bool? Presence {get;set;}

        [ForeignKey("IdClient")]
        public Client Client { get; set; }

        [ForeignKey("IdTraining")]
        public ScheduleTraining ScheduleTraining { get; set; }
    }
}
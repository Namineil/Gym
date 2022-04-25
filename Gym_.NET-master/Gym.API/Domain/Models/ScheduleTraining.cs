using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.API.Domain.Models
{

    public class ScheduleTraining
    {
        public int IdTraining { get; set; }

        public int IdTrainer { get; set; }

        public int IdRoom { get; set; }

        public DateTimeOffset TrainingDateFrom { get; set; }

        public DateTimeOffset TrainingDateTo { get; set; }

        public int IdSpecialization { get; set; }

        public string Type { get; set; }

        public int Roominess { get; set; }

        public int Engaged { get; set; }

        public bool RecordIsClosed { get; set; }

        public IList<ClassRecords> ClassRecords { get; set; } = new List<ClassRecords>();
        [ForeignKey("IdRoom")]
        public Room Room { get; set; }
        [ForeignKey("IdSpecialization")]
        public Specialization Specialization { get; set; }
        [ForeignKey("IdTrainer")]
        public Trainer Trainer { get; set; }
    }

}
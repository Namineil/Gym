using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.API.Domain.Models
{

    public class Trainer
    {
        public int IdTrainer { get; set; }

        public int IdSpecializations { get; set; }

        public string Status { get; set; }

        public int IdUser { get; set; }

        public IList<ScheduleTraining> ScheduleTraining { get; set; } = new List<ScheduleTraining>();
        [ForeignKey("IdUser")]
        public User User { get; set; }
    }

}
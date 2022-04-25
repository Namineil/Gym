using System.Collections.Generic;

namespace Gym.API.Domain.Models
{

    public class Specialization
    {
        public int IdSpecialization { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }

        public IList<ScheduleTraining> ScheduleTraining { get; set; } = new List<ScheduleTraining>();

    }
}
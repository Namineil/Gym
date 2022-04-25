using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.API.Domain.Models
{

    public class Room
    {
        public int IdRoom { get; set; }

        public string Name { get; set; }

        public int IdAdministrator { get; set; }
        [ForeignKey("IdAdministrator")]
        public AdministratorHall AdministratorHall { get; set; }

        public IList<ScheduleTraining> ScheduleTraining { get; set; } = new List<ScheduleTraining>();
    }

}
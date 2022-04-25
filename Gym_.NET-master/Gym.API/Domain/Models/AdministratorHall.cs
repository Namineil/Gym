using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.API.Domain.Models
{
    public class AdministratorHall
    {
        public int IdAdministrator { get; set; }

        public string Status { get; set; }

        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }

        public IList<Room> Room { get; set; } = new List<Room>();
    }
}

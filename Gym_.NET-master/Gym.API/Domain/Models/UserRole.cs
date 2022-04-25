using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.API.Domain.Models
{

    public class UserRole
    {
        public int IdUserRole { get; set; }
        public int IdRole { get; set; }

        [ForeignKey("IdRole")]
        public Role Role { get; set; }

        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }
    }

}
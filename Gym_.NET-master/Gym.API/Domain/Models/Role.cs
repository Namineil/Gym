using System.Collections.Generic;

namespace Gym.API.Domain.Models
{

    public class Role
    {
        public int IdRole { get; set; }

        public string Name { get; set; }

        public IList<UserRole> UserRole { get; set; } = new List<UserRole>();
       }

}
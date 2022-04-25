using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class SaveRoomResource
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public string AdministratorName { get; set; }
    }

}
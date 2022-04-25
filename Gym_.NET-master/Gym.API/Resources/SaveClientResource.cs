using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class SaveClientResource
    {

        [Required]
        [MaxLength(150)]
        public string Status { get; set; }

        [Required]
        [MaxLength(150)]
        public string UserName { get; set; }

        public string Card { get; set; }

    }
}
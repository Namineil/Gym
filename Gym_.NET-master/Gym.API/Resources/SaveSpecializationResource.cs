using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class SaveSpecializationResource
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public string Image { get; set; }

    }
}
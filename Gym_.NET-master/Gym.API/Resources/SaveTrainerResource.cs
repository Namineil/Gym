using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class SaveTrainerResource
    {
        public string SpecializationsName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Status { get; set; }
    }

}
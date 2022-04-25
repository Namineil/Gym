using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class SaveUserResource
    {
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Gender { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(150)]
        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public string Image { get; set; }
        
        [Required]
        [MaxLength(150)]
        public string Login { get; set; }

        [Required]
        [MaxLength(150)]
        public string Password { get; set; }
    }

}
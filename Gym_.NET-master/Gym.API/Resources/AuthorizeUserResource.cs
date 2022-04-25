using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class AuthorizeUserResource
    {
        
        [Required]
        [MaxLength(150)]
        public string Login { get; set; }

        [Required]
        [MaxLength(150)]
        public string Password { get; set; }
        public bool RememberMe {get; set;}
    }

}
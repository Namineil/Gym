using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class ValidateTokenResource
    {
        [Required]
        public string Token { get; set; }
    }

}
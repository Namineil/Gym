using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class SaveNewsResource
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
    }
}
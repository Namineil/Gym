using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class SaveClassRecordsResource
    {
        [Required]
        public int IdTraining { get; set; }

        [Required]
        public int IdClient { get; set; }
        
        public bool Presence {get;set;}
    }
}
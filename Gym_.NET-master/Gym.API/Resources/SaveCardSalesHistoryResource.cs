using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.API.Resources
{

    public class SaveCardSalesHistoryResource
    {

        [Required]
        public int IdUser { get; set; }

        [Required]
        public int IdCard { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
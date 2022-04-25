using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.API.Domain.Models
{

    public class ServicesCard
    {
        public int IdServices { get; set; }

        public int IdCard { get; set; }

        public string Name { get; set; }
        [ForeignKey("IdCard")]
        public Card Card { get; set; }
    }

}
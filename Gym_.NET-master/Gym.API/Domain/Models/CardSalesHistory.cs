using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.API.Domain.Models
{

    public class CardSalesHistory
    {
        public int IdSalesHistory { get; set; }
        public int IdUser { get; set; }

        public int IdCard { get; set; }
        public DateTime Date { get; set; }

       [ForeignKey("IdCard")]
       public Card Card { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.API.Domain.Models
{

    public class Client
    {
        public int IdClient { get; set; }

        public string Status { get; set; }

        public int IdCard { get; set; }

        public int IdUser { get; set; }
        [ForeignKey("IdCard")]
        public Card Card { get; set; }

        public IList<ClassRecords> ClassRecords { get; set; } = new List<ClassRecords>();
        [ForeignKey("IdUser")]
        public User User { get; set; }
    }
}
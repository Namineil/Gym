using System.Numerics;
using System.Collections.Generic;

namespace Gym.API.Domain.Models
{

    public class Card
    {
        public int IdCard { get; set; }
        public string Name { get; set; }

        public long Period { get; set; }
        public decimal Price { get; set; }
        public string Image {get; set;}

        public IList<Client> Client {get;set;} = new List<Client>();

        public IList<ServicesCard> ServicesCard {get;set;} = new List<ServicesCard>();
    }
}
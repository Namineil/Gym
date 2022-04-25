using System.Collections.Generic;

namespace Gym.API.Resources
{

    public class CardResource
    {
        public int IdCard { get; set; }
        public string Name { get; set; }

        public long Period { get; set; }
        public decimal Price { get; set; }
        public string Image {get; set;}
    }
}
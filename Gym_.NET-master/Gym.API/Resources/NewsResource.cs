using System;
using System.Collections.Generic;

namespace Gym.API.Resources
{

    public class NewsResource
    {
        public int IdNews { get; set; }
        public string Title { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
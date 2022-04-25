using System;
using System.Numerics;
using System.Collections.Generic;

namespace Gym.API.Domain.Models
{

    public class News
    {
        public int IdNews { get; set; }
        public string Title { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }

    }
}
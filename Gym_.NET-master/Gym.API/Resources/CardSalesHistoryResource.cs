using System;
using System.Collections.Generic;

namespace Gym.API.Resources
{

    public class CardSalesHistoryResource
    {
        public int IdSalesHistory { get; set; }
        public int IdUser { get; set; }

        public int IdCard { get; set; }
        public DateTime Date { get; set; }
    }
}
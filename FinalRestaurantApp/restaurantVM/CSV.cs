using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.restaurantVM
{
    public class CSV
    {
        public string Name { get; set; }
        public int NumOfOrders { get; set; }
        public float SumPriseInUsd { get; set; }
        public float SumPriseInNis { get; set; }
        public string BestMeal { get; set; }
        public string MostCustomerBuy { get; set; }
    }
}

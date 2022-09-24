using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.restaurantVM.RestaurantMenu
{
    public class AddRestaurantMenuVMcs
    {
        public string MealName { get; set; }
        public float PriseInNis { get; set; }
        public int Quantity { get; set; }
        public int RestaurantId { get; set; }
    }
}

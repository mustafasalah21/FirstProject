using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.restaurantVM.RestaurantMenu
{
    public class RestaurantMenuVM
    {
        public RestaurantMenuVM()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = null;
        }
        public int Id { get; set; }
        public string MealName { get; set; }
        public float PriseInUsd { get; set; }
        public float PriseInNis { get; set; }
        public int Quantity { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Archived { get; set; }
    }
}

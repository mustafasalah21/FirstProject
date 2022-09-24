using System;
using System.Collections.Generic;

#nullable disable

namespace FinalRestaurantApp.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantMenuId { get; set; }
        public int Quantity { get; set; }
        public float PriseInNis { get; set; }
        public float PriseInUsd { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual RestaurantMenu RestaurantMenu { get; set; }
    }
}

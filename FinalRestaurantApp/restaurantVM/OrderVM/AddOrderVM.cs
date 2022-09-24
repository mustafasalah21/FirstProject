using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.restaurantVM.OrderVM
{
    public class AddOrderVM
    {
        public int CustomerId { get; set; }
        public int RestaurantMenuId { get; set; }
        public int Quantity { get; set; }
    }
}

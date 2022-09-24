using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.restaurantVM.OrderVM
{
    public class UpdateOrderVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantMenuId { get; set; }
    }
}

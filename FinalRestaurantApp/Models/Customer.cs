using System;
using System.Collections.Generic;

#nullable disable

namespace FinalRestaurantApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Archived { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

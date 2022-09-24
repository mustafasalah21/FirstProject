using FinalRestaurantApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantApp.restaurantVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalRestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsControllercs : ControllerBase
    {
        private readonly RestaurantDbContext _db;
        public ReportsControllercs(RestaurantDbContext db)
        {
            _db = db;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            var reportData = _db.Restaurants.Select(x => new CSV
            {
                Name = x.Name,
                NumOfOrders = _db.Orders.Count(z => _db.RestaurantMenus.Where(a => a.RestaurantId == x.Id).Select(d => d.Id)
                                                                       .Any(w => w == z.RestaurantMenuId)),

                SumPriseInNis = _db.Orders.Where(z => _db.RestaurantMenus.Where(a => a.RestaurantId == x.Id).Select(d => d.Id)
                                                                       .Any(w => w == z.RestaurantMenuId)).Sum(r => r.PriseInNis),

                SumPriseInUsd = _db.Orders.Where(z => _db.RestaurantMenus.Where(a => a.RestaurantId == x.Id).Select(d => d.Id)
                                                                      .Any(w => w == z.RestaurantMenuId)).Sum(r => r.PriseInUsd),
            });
            return Ok(reportData);
        }
    }
}


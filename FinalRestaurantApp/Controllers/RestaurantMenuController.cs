using FinalRestaurantApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantApp.restaurantVM.RestaurantMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalRestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantMenuController : ControllerBase
    {
        private readonly RestaurantDbContext _db;
        public RestaurantMenuController(RestaurantDbContext db)
        {
            _db = db;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            var data = _db.RestaurantMenus.Select(x => new RestaurantMenuVM
            {
                Id = x.Id,
                MealName = x.MealName,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Archived = x.Archived,
                PriseInNis = x.PriseInNis,
                PriseInUsd = x.PriseInUsd,
                Quantity = x.Quantity
            });
            return Ok(data);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _db.RestaurantMenus.Where(a => a.Id == id).ToList();
            return Ok(data);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] AddRestaurantMenuVMcs x)
        {
            var data = new RestaurantMenu
            {
                MealName = x.MealName,
                PriseInNis = x.PriseInNis,
                PriseInUsd = (float)(x.PriseInNis / 3.5),
                Quantity = x.Quantity,
                RestaurantId = x.RestaurantId
            };
            _db.RestaurantMenus.Add(data);
            _db.SaveChanges();
            return Ok("Added Succeeded");
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(UpdateRestaurantMenuVMcs model)
        {
            var data = _db.RestaurantMenus.Find(model.Id);
            if (model.MealName != "string")
            {
                data.MealName = model.MealName;
            }
            if (model.PriseInNis != 0)
            {
                data.PriseInNis = model.PriseInNis;
                data.PriseInUsd = (float)(model.PriseInNis / 3.5);
            }
            if (model.Quantity != 0)
            {
                data.Quantity = model.Quantity;
            }
            data.UpdatedDate = DateTime.Now;
            _db.SaveChanges();
            return Ok("Updated Succeeded");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _db.RestaurantMenus.Find(id);
            if (data is not null)
            {
                _db.RestaurantMenus.Remove(data);
                _db.SaveChanges();
                return Ok("Delete Succeeded");
            }
            return Ok("Item Not Found");
        }
    }
}


using FinalRestaurantApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _db;
        public RestaurantController(RestaurantDbContext db)
        {
            _db = db;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            var data2 = _db.Restaurants.ToList();
            return Ok(data2);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _db.Restaurants.Where(a => a.Id == id).ToList();
            return Ok(data);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] AddRestaurantVM model)
        {
            var data = new Restaurant
            {
                Name = char.ToUpper(model.Name[0]) + model.Name.Substring(1) ,
                PhoneNumber = model.PhoneNumber,
            };
            _db.Restaurants.Add(data);
            _db.SaveChanges();
            return Ok("Added Succeeded");
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(UpdateRestaurantVM model)
        {
            var data = _db.Restaurants.Find(model.Id);
            if (model.Name != "string")
            {
                data.Name = char.ToUpper(model.Name[0]) + model.Name.Substring(1);
            }
            if (model.PhoneNumber != "string")
            {
                data.PhoneNumber = model.PhoneNumber;
            }
            data.UpdatedDate = DateTime.Now;
            _db.SaveChanges();
            return Ok("Updated Succeeded");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _db.Restaurants.Find(id);
            if (data is not null)
            {
                _db.Restaurants.Remove(data);
                _db.SaveChanges();
                return Ok("Delete Succeeded");
            }
            return Ok("Item Not Found");
        }
    }
}


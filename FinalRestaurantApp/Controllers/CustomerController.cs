using FinalRestaurantApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantApp.restaurantVM.CustomerVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalRestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly RestaurantDbContext _db;
        public CustomerController(RestaurantDbContext db)
        {
            _db = db;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            var data = _db.Customers.Select(x => new CustomerVM
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Archived = x.Archived
            });
            return Ok(data);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _db.Customers.Where(a => a.Id == id).ToList();
            return Ok(data);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerVM model)
        {
            var data = new Customer
            {
                FirstName = char.ToUpper(model.FirstName[0]) + model.FirstName.Substring(1),
                LastName = char.ToUpper(model.LastName[0]) + model.LastName.Substring(1),
            };
            _db.Customers.Add(data);
            _db.SaveChanges();
            return Ok("Added Succeeded");
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(UpdateCustomerVM model)
        {
            var data = _db.Customers.Find(model.Id);
            if (model.FirstName != "string")
            {
                data.FirstName = char.ToUpper(model.FirstName[0]) + model.FirstName.Substring(1);
            }
            if (model.LastName != "string")
            {
                data.LastName = char.ToUpper(model.LastName[0]) + model.LastName.Substring(1);
            }
            data.UpdatedDate = DateTime.Now;
            _db.SaveChanges();
            return Ok("Updated Succeeded");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _db.Customers.Find(id);
            if (data is not null)
            {
                _db.Customers.Remove(data);
                _db.SaveChanges();
                return Ok("Delete Succeeded");
            }
            return Ok("Item Not Found");
        }
    }
}


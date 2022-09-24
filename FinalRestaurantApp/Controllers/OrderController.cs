using FinalRestaurantApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantApp.restaurantVM.OrderVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalRestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly RestaurantDbContext _db;
        public OrderController(RestaurantDbContext db)
        {
            _db = db;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            var data = _db.Orders.ToList();
            return Ok(data);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _db.RestaurantMenus.Find(id);
            return Ok(data);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] AddOrderVM model)
        {
            var dataModel = new Order();
            var dataDb = _db.RestaurantMenus.Find(model.RestaurantMenuId);
            if (dataDb is not null)
            {
                if (model.Quantity > dataDb.Quantity)
                {
                    return Ok("Not Found Enough Quantity!");
                }

                dataDb.Quantity = dataDb.Quantity - model.Quantity;

                var orderStatus = _db.Orders.Where(x => x.CustomerId == model.CustomerId && x.RestaurantMenuId == model.RestaurantMenuId).FirstOrDefault();
                if (orderStatus is not null)
                {
                    orderStatus.Quantity = orderStatus.Quantity + model.Quantity;
                    orderStatus.PriseInNis = orderStatus.Quantity * dataDb.PriseInNis;
                    orderStatus.PriseInUsd = (float)((orderStatus.Quantity * dataDb.PriseInNis) / 3.5);
                }
                else
                {
                    dataModel.CustomerId = model.CustomerId;
                    dataModel.RestaurantMenuId = model.RestaurantMenuId;
                    dataModel.Quantity = model.Quantity;
                    dataModel.PriseInNis = model.Quantity * dataDb.PriseInNis;
                    dataModel.PriseInUsd = (float)((model.Quantity * dataDb.PriseInNis) / 3.5);
                    _db.Orders.Add(dataModel);
                }
                _db.SaveChanges();
                return Ok("Added Succeeded");
            }
            var data = new Order
            {
                CustomerId = model.CustomerId,
                RestaurantMenuId = model.RestaurantMenuId,
                Quantity = model.Quantity
            };
            _db.Orders.Add(data);
            _db.SaveChanges();
            return Ok("Added Succeeded");
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(UpdateOrderVM model)
        {
            var data = _db.Orders.Find(model.Id);
            if (model.CustomerId != 0 && model.RestaurantMenuId != 0 && data is not null)
            {
                data.CustomerId = model.CustomerId;
                data.RestaurantMenuId = model.RestaurantMenuId;
                _db.SaveChanges();
                return Ok("Updated Succeeded");
            }
            return Ok("Found Error");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _db.Orders.Find(id);
            if (data is not null)
            {
                _db.Orders.Remove(data);
                _db.SaveChanges();
                return Ok("Delete Succeeded");
            }
            return Ok("Item Not Found");
        }
    }
}


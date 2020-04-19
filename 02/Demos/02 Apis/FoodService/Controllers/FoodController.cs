using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FoodService
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        public FoodController()
        {
        }

        // GET api/food
        [HttpGet("")]
        public ActionResult<IEnumerable<FoodItem>> GetFood()
        {
            return new List<FoodItem> { new FoodItem { id = 1, name = "Lasagne", price = 2, calories = 300 }, new FoodItem { id = 2, name = "'Schnitzel'", price = 2, calories = 300 } };
        }

        // GET api/food/5
        [HttpGet("{id}")]
        public ActionResult<FoodItem> GetstringById(int id)
        {
            return null;
        }

        // POST api/food
        [HttpPost("")]
        public void Poststring(FoodItem value)
        {
        }

        // PUT api/food/5
        [HttpPut("{id}")]
        public void Putstring(int id, FoodItem value)
        {
        }

        // DELETE api/food/5
        [HttpDelete("{id}")]
        public void DeletestringById(int id)
        {
        }
    }
}
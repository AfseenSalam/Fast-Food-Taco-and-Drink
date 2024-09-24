using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAll(string? sortByCost = null)
        {
            List<Drink> result = dbContext.Drinks.ToList();
            if (sortByCost != null)
            {
                if (sortByCost.ToLower().Trim() == "ascending")
                {
                    result = result.OrderBy(d => d.Cost).ToList();
                }
                else
                {
                    result = result.OrderByDescending(d => d.Cost).ToList();
                }

            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Drink d = dbContext.Drinks.FirstOrDefault(d => d.Id == id);
            if (d == null)
            {
                return NotFound("Drink is not found");
            }
            else
            {
                return Ok(d);
            }
        }
        [HttpPost()]
        public IActionResult AddDrink([FromBody] Drink newDrink)
        {
            newDrink.Id = 0;
            dbContext.Drinks.Add(newDrink);
            dbContext.SaveChanges();
            return Created($"/api/Drink/{newDrink.Id}", newDrink);
        }

        [HttpPut()]
        public IActionResult UpdateDrinks(int id, [FromBody]Drink newDrink)
        {
            if (id != newDrink.Id) { return BadRequest("Ids don't match"); }
            if (dbContext.Drinks.Any(u => u.Id == id) == false) { return NotFound("No matching ids"); }
            dbContext.Drinks.Update(newDrink);
            dbContext.SaveChanges();
            return Ok(newDrink);
        }
    }
}

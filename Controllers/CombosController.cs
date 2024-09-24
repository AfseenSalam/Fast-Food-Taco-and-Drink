using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombosController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();
        [HttpGet()]
        public IActionResult GetAll()
        {
            List<Combo> result = dbContext.Combos
                                    .Include(c => c.Drink)
                                    .Include(t => t.Taco)
                                    .ToList();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Combo d = dbContext.Combos
                                    .Include(c => c.Drink)
                                    .Include(t => t.Taco)
                                    .FirstOrDefault(d => d.Id == id);
            if (d == null)
            {
                return NotFound("Combo is not found");
            }
            else
            {
                return Ok(d);
            }
        }
        [HttpPost()]
        public IActionResult AddCombo([FromBody] Combo newCombo)
        {
            newCombo.Id = 0;
            dbContext.Combos.Add(newCombo);
            dbContext.SaveChanges();
            return Created($"/api/Drink/{newCombo.Id}", newCombo);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTaco(int id)
        {
            Combo c = dbContext.Combos.FirstOrDefault(t => t.Id == id);
            if (c == null)
            {
                return NotFound("Taco not found");
            }
            else
            {
                //List<Drink> matchingDrink = dbContext.Drinks.Where(t => t.Id == c.Id).ToList();
                //dbContext.Drinks.RemoveRange(matchingDrink);
               
                dbContext.Combos.Remove(c);
                dbContext.SaveChanges();
                return NoContent();
            }
        }
    }
}

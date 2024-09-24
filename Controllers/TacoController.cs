using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacoController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAll(string apiKey, bool? softShell = null)
        {
            List<Taco> result = dbContext.Tacos.ToList();
            if (!UserDAl.ValidateKey(apiKey))
            {
                return Unauthorized();
            }
            if (softShell !=null) {
                result = result.Where(t => t.SoftShell == softShell).ToList();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id, string apiKey)
        {
            Taco result = dbContext.Tacos.FirstOrDefault(t => t.Id == id);
            if (!UserDAl.ValidateKey(apiKey))
            {
                return Unauthorized();
            }
            if (result == null)
            {
                return NotFound("Taco is not found");
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost()]
        public IActionResult AddTaco(string apiKey,[FromBody]Taco newTaco)
        {
            if (!UserDAl.ValidateKey(apiKey))
            {
                return Unauthorized();
            }
            newTaco.Id = 0;
            dbContext.Tacos.Add(newTaco);
            dbContext.SaveChanges();
            return Created($"/api/Taco/{newTaco.Id}",newTaco);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaco(string apiKey,int id)
        {
            Taco s = dbContext.Tacos.FirstOrDefault(t=>t.Id == id);
            if (!UserDAl.ValidateKey(apiKey))
            {
                return Unauthorized();
            }
            if (s == null)
            {
                return NotFound("Taco not found");
            }
            else
            {
                List<Taco> matchingTaco = dbContext.Tacos.Where(t => t.Id == s.Id).ToList();
                dbContext.Tacos.RemoveRange(matchingTaco);
                dbContext.Tacos.Remove(s);
                dbContext.SaveChanges();
                return NoContent();
            }
        }
    }
}

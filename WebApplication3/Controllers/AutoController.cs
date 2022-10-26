using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoController : Controller
    {
        private readonly ApplicationContext _context;

        public AutoController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _context.Autos.ToListAsync();

            return Ok(response);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _context.Autos.FindAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Auto auto)
        {
            if (auto == null)
            {
                return BadRequest();
            }
            var response = await _context.Autos.AddAsync(auto);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Auto auto)
        {
            if (auto == null)
            {
                return BadRequest();
            }

            _context.Autos.Update(auto);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _context.Autos.FindAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            _context.Autos.Remove(response);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}

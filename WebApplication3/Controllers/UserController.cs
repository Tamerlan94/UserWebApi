using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _context.Users.ToListAsync();

            return Ok(response);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _context.Users.FindAsync(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var response = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest();
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _context.Users.FindAsync(id);
            if(response == null)
            {
                return NotFound();
            }

            _context.Users.Remove(response);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}

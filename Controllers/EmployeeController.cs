using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApplication.DB;

namespace SampleApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EMS_NEWContext _context;

        public EmployeeController(EMS_NEWContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();
            var product = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product);

        }


        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {
            if (user == null || user.Id == 0)
                return BadRequest();

            var product = await _context.Users.FindAsync(user.Id);
            if (product == null)
                return NotFound();
            product.FirstName = user.FirstName;
            product.LastName = user.LastName;
            product.Email = user.Email;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            var users = await _context.Users.FindAsync(id);
            if (users == null)
                return NotFound();
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return Ok();

        }



    }
}

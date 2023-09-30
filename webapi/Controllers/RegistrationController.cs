using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly EventRegistrationDbContext _context;
        
        public RegistrationController(EventRegistrationDbContext context)
        {
            _context = context;
        }

        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
            return await _context.Registrations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetRegistration(Guid id)
        {
            var registration = _context.Registrations.FirstOrDefault(reg => reg.Id == id);
            if (registration == null)
            {
                return NoContent();
            }

            return Ok(registration);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistration(Guid id, Registration reg)
        {
            if (id != reg.Id)
            {
                return BadRequest();
            }

            _context.Entry(reg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var registrationExists = _context.Registrations.Any(e => e.Id == id);
                if (!registrationExists)
                {
                    return NotFound();
                }
                throw;
            }
            
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Registration>> PostRegistration(Registration reg)
        {
            _context.Registrations.Add(reg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistration", new { id = reg.Id }, reg);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(Guid id)
        {
            var reg = await _context.Registrations.FirstOrDefaultAsync(evt => evt.Id == id);

            if (reg == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(reg);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
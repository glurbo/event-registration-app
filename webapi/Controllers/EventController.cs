using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventRegistrationDbContext _context;
        
        public EventController(EventRegistrationDbContext context)
        {
            _context = context;
        }

        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.Include(x => x.Registrations).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var evt = await _context.Events.FirstOrDefaultAsync(evt => evt.Id == id);
            if (evt == null)
            {
                return NoContent();
            }

            return Ok(evt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, Event evt)
        {
            if (id != evt.Id)
            {
                return BadRequest();
            }

            _context.Entry(evt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var eventExists = _context.Events.Any(e => e.Id == id);
                if (!eventExists)
                {
                    return NotFound();
                }
                throw;
            }
            
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event evt)
        {
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = evt.Id }, evt);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var evt = await _context.Events.FirstOrDefaultAsync(evt => evt.Id == id);

            if (evt == null)
            {
                return NotFound();
            }

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

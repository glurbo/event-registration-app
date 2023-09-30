using DAL;
using DTO.Public.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventRegistrationDbContext _context;
        private readonly EventMapper _mapper = new EventMapper();
        
        public EventController(EventRegistrationDbContext context)
        {
            _context = context;
        }

        [HttpGet]   
        public async Task<ActionResult<IEnumerable<DTO.Public.Event>>> GetEvents()
        {
            var events = await _context.Events.Include(x => x.Registrations).ToListAsync();
            var res = events.Select(x => _mapper.Map(x)).ToList();
            return res;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTO.Public.Event>> GetEvent(Guid id)
        {
            var evt = await _context.Events
                .Include(x => x.Registrations)
                .FirstOrDefaultAsync(evt => evt.Id == id);
            if (evt == null)
            {
                return NoContent();
            }

            var res = _mapper.Map(evt);
            return Ok(res);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, DTO.Public.Event evt)
        {
            if (id != evt.Id)
            {
                return BadRequest();
            }
            
            var res = _mapper.Map(evt);
            _context.Events.Update(res);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DTO.Public.Event>> PostEvent(DTO.Public.Event evt)
        {
            var res = _mapper.Map(evt);
            _context.Events.Add(res);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = evt.Id }, evt);
        }

        [Authorize]
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

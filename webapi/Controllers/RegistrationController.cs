using DAL;
using Domain;
using DTO.Public.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly EventRegistrationDbContext _context;
        private readonly RegistrationMapper _mapper = new RegistrationMapper();

        
        public RegistrationController(EventRegistrationDbContext context)
        {
            _context = context;
        }

        [HttpGet]   
        public async Task<ActionResult<IEnumerable<DTO.Public.Registration>>> GetRegistrations()
        {
            var registrations = await _context.Registrations.ToListAsync();
            var res = registrations.Select(x => _mapper.Map(x)).ToList();
            return res;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTO.Public.Registration>> GetRegistration(Guid id)
        {
            var registration = await _context.Registrations.FirstOrDefaultAsync(reg => reg.Id == id);
            if (registration == null)
            {
                return NoContent();
            }
            
            var res = _mapper.Map(registration);
            return Ok(res);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistration(Guid id, DTO.Public.Registration reg)
        {
            if (id != reg.Id)
            {
                return BadRequest();
            }

            var res = _mapper.Map(reg);
            _context.Registrations.Update(res);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<DTO.Public.Registration>> PostRegistration(string eventId, DTO.Public.Registration registration)
        {
            var res = _mapper.Map(registration);
            res.EventId = Guid.Parse(eventId);
            _context.Registrations.Add(res);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistration", new { id = registration.Id }, registration);

        }

        [Authorize]
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
using DAL;
using Domain;
using DTO.Public.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly EventRegistrationDbContext _context;
        private readonly RegistrationMapper _mapper = new RegistrationMapper();

        
        public RegistrationController(EventRegistrationDbContext context)
        {
            _context = context;
        }

        [HttpGet]   
        public async Task<ActionResult<IEnumerable<DTO.Public.Registration>>> GetRegistrations(string eventId)
        {
            var registrations = await _context.Registrations.Where(x => x.EventId.ToString() == eventId).ToListAsync();
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
        public async Task<IActionResult> PutRegistration(string id, DTO.Public.Registration reg)
        {
            if (Guid.Parse(id) != reg.Id)
            {
                return BadRequest();
            }

            var res = _mapper.Map(reg);
            _context.Registrations.Update(res);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<DTO.Public.Registration>> PostRegistration(string eventId, [FromBody] DTO.Public.RegisterData registration)
        {
            var result = new Domain.Registration()
            {
                EventId = Guid.Parse(eventId),
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                IdentificationCode = registration.IdentificationCode
            };
            _context.Registrations.Add(result);
            await _context.SaveChangesAsync();

            var returnVal = _mapper.Map(result);

            return CreatedAtAction("GetRegistration", new { id = returnVal.Id }, returnVal);

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
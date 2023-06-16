using Microsoft.AspNetCore.Mvc;
using EntryLog.Entities;
using EntryLog.Enums;
using EntryLog.Data;

namespace EntryLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryLogsController : ControllerBase
    {
        private readonly EntryLogDbContext _dbContext;

        public EntryLogsController(EntryLogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/EntryLogs
        [HttpGet]
        public ActionResult<IQueryable<PeopleEntryLogs>> Get()
        {
            var people = _dbContext.PeopleEntryLogs;
            return Ok(people);
        }

        // GET api/EntryLogs/{id}
        [HttpGet("{id}")]
        public ActionResult<PeopleEntryLogs> Get(int id)
        {
            var person = _dbContext.PeopleEntryLogs.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // POST api/EntryLogs
        [HttpPost]
        public ActionResult<PeopleEntryLogs> Post([FromBody] PeopleEntryLogs person)
        {
            if (!Enum.IsDefined(typeof(Gender), person.Gender))
            {
                return BadRequest("Invalid enum value for EnumProperty.");
            }

            _dbContext.PeopleEntryLogs.Add(person);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }

        // PUT api/EntryLogs/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PeopleEntryLogs updatedPerson)
        {
            var person = _dbContext.PeopleEntryLogs.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            if (!Enum.IsDefined(typeof(Gender), updatedPerson.Gender))
            {
                return BadRequest("Invalid enum value for EnumProperty.");
            }

            if (updatedPerson.Gender > Gender.Other)
            {
                return BadRequest("Enum value for Gender is higher than the allowed threshold.");
            }

            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.Address = updatedPerson.Address;
            person.PhoneNumber = updatedPerson.PhoneNumber;
            person.Gender = updatedPerson.Gender;

            _dbContext.SaveChanges();

            return NoContent();
        }


        // DELETE api/EntryLogs/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _dbContext.PeopleEntryLogs.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            _dbContext.PeopleEntryLogs.Remove(person);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}

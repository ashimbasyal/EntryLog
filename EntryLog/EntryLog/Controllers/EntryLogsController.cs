using Microsoft.AspNetCore.Mvc;
using EntryLog.Entities;
using System.Collections.Generic;
using EntryLog.Enums;

namespace EntryLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryLogsController : ControllerBase
    {
        private static List<PeopleEntryLogs> entryLog = new List<PeopleEntryLogs>();

        // GET api
        [HttpGet]
        public ActionResult<IEnumerable<PeopleEntryLogs>> Get()
        {
            return Ok(entryLog);
        }

        // GET api/Entrylogs/{id}
        [HttpGet("{id}")]
        public ActionResult<PeopleEntryLogs> Get(int id)
        {
            var person = entryLog.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public ActionResult<PeopleEntryLogs> Post([FromBody] PeopleEntryLogs person)
        {
            if (!Enum.IsDefined(typeof(Gender), person.Gender))
            {
                return BadRequest("Invalid enum value for EnumProperty.");
            }

            person.Id = entryLog.Count + 1;
            entryLog.Add(person);
            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PeopleEntryLogs updatedPerson)
        {
            var person = entryLog.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.Address = updatedPerson.Address;
            person.PhoneNumber = updatedPerson.PhoneNumber;
            person.Gender = updatedPerson.Gender;

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = entryLog.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            entryLog.Remove(person);

            return NoContent();
        }
    }
}
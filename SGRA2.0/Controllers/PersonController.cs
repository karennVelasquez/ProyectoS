using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGRA2._0.Model;
using SGRA2._0.Service;
using System.Numerics;
using System.Reflection.Metadata;
namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        //GET api/<Person>
        [HttpGet]
        public async Task<ActionResult<List<Person>>>GetAllPerson()
        {
            return Ok(await _personService.GetAll());
        }

        //GET api/<Person>/
        [HttpGet("{IdPerson}")]
        public async Task<ActionResult<Person>>GetPerson(int IdPerson)
        {
            var Person = await _personService.GetPerson(IdPerson); 
            if (Person == null) 
            {
                return BadRequest("Person not found");
            }
            return Ok(Person);
        }

        //POST api/<Person>
        [HttpPost]
        public async Task<ActionResult<Person>>PostPerson(int IdPerson, string Name, string Lastname, string Email, int IdDocumentType, int Document)
        {
            var personToPut = await _personService.CreatePerson(Name, Lastname, Email, IdDocumentType, Document);
            if (personToPut != null) 
            {
                return Ok(personToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database.");
            }
        }

        //PUT api/<Person>
        [HttpPut("Update/{IdPerson}")]
        public async Task<ActionResult<Person>> PutPerson(int IdPerson, string Name, string Lastname, string Email, int IdDocumentType, int Document)
        {
            var personToPut = _personService.UpdatePerson(IdPerson, Name, Lastname, Email, IdDocumentType, Document);
            if (personToPut != null) 
            {
                return Ok(personToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<Person>
        [HttpPut("Delete/{IdPerson}")]
        public async Task<ActionResult<Person>> DeletePerson(int IdPerson)
        {
            var personToDelete = await _personService.DeletePerson(IdPerson);  
            if (personToDelete != null) 
            {
                return Ok(personToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

    }
}

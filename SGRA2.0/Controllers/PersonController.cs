using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGRA2._0.Service;
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
        [HttpGet]
        public async Task<ActionResult<List<Person>>>GetAllPerson()
        {
            return Ok(await _personService.GetAll());
        }
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
        [HttpPost]
        public async Task<ActionResult<Person>>CreatePerson(string Name, string Lastname, string Email, int IdDocumentType, int Document)
        {
            var Person = await _personService.CreatePerson(Name, Lastname, Email, IdDocumentType, Document);
           
            return Ok(Person);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGRA2._0.Model;
using SGRA2._0.Service;
using System.Numerics;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
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

        //GET api/<PersonController>
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetAllPerson()
        {
            return Ok(await _personService.GetAll());
        }

        //GET api/<PersonController>/
        [HttpGet("{IdPerson}")]
        public async Task<ActionResult<Person>> GetPerson(int IdPerson)
        {
            var person = await _personService.GetPerson(IdPerson);
            if (person == null)
            {
                return BadRequest("Waste type noy found. ");
            }
            return Ok(person);
        }

        //POST api/<PersonController>
        [HttpPost("{n}")]
        public async Task<ActionResult<Person>> PostPerson(int IdPerson, string Name, string Lastname, string Email, int IdDocumentType, int Document)
        {
            var personToPut = _personService.CreatePerson(IdPerson, Name, Lastname, Email, IdDocumentType, Document);
            if (personToPut != null)
            {
                return Ok(personToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<PersonController>
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

        //DELETE api/<PersonController>
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
/*
using HClinicalV2._0.Model;
using HClinicalV2._0.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Threading.ExecutionContext;
using Microsoft.AspNetCore.Http;
using static Azure.Core.HttpHeader;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HClinicalV2._0.Controllers
{
    [Route("HClinical/[controller]")]
    [ApiController]
    public class EpsController : ControllerBase
    {
        private readonly IEpsService _epsService;
        public EpsController(IEpsService epsService)
        {
            _epsService = epsService;
        }


        // GET: api/<epsController>
        [HttpGet]
        public async Task<ActionResult<List<Eps>>> GetAllEps()
        {
            return Ok(await _epsService.GetAll());
        }

        // GET api/<epsController>/5
        [HttpGet("{idEps}")]
        public async Task<ActionResult<Eps>> GetEps(int idEps)
        {
            var eps = await _epsService.GetEps(idEps);
            if (eps == null)
            {
                return BadRequest("Eps not found");
            }
            return Ok(eps);
        }

        // POST: api/Eps
        [HttpPost("{nameEps}")]
        public async Task<ActionResult<Eps>> PostEps(string nameEps)
        {
            var epsToPut = _epsService.CreateEps(nameEps);

            if (epsToPut != null)
            {
                return Ok(epsToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database");
            }


        }

        // PUT: api/Eps/5
        [HttpPut("Update/{idEps}")]
        public async Task<ActionResult<Eps>> PutEps(int idEps, string nameEps)
        {
            var epsToPut = await _epsService.UpdateEps(idEps, nameEps);

            if (epsToPut != null)
            {
                return Ok(epsToPut);
            }
            else
            {
                return BadRequest("Error updating the database :(");
            }

        }

        // Delete: api/Eps/5
        [HttpPut("Delete/{idEps}")]
        public async Task<ActionResult<Eps>> DeleteEps(int idEps)
        {

            var epsToDelete = await _epsService.DeleteEps(idEps);

            if (epsToDelete != null)
            {
                return Ok(epsToDelete);
            }
            else
            {
                return BadRequest("Error updating the database");
            }
        }
    }
}*/

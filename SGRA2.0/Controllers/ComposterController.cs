using SGRA2._0.Model;
using SGRA2._0.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Threading.ExecutionContext;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ComposterController : ControllerBase
    {
        public readonly IComposterService _composterService;

        public ComposterController(IComposterService composterService)
        {
            _composterService = composterService;
        }

        //GET api/<ComposterController>
        [HttpGet]
        public async Task<ActionResult<Composter>> GetAllComposter()
        {
            return Ok(await _composterService.GetAll());
        }
        
        //GET api/<ComposterController>
        [HttpGet("{IdComposter}")]
        public async Task<ActionResult<Composter>> GetComposter(int IdComposter)
        {
            var Composter = await _composterService.GetComposter(IdComposter);
            if(Composter == null)
            {
                return BadRequest("Composter not found.");
            }
            return Ok(Composter);
        }

        //POST api/<ComposterController>
        [HttpPost("Create/")]
        public async Task<ActionResult<Composter>> PostComposter( string Material, string DrainageSystem)
        {
            var ComposterToPut = await _composterService.CreateComposter(Material, DrainageSystem);  
            if(ComposterToPut != null)
            {
                return Ok(ComposterToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<ComposterController>
        [HttpPut("Update/{IdComposter}")]
        public async Task<ActionResult<Composter>> PutComposter(int IdComposter, string Material, string DrainageSystem)
        {
            var ComposterToPut = await _composterService.UpdateComposter(IdComposter, Material, DrainageSystem);
            if(ComposterToPut != null)
            {
                return Ok(ComposterToPut);
            }
            else 
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<ComposterController>
        [HttpPut("Delete/{IdComposter}")]
        public async Task<ActionResult<Composter>> DeleteComposter(int IdComposter)
        {
            var ComposterToDelete = await _composterService.DeleteComposter(IdComposter);   
            if(ComposterToDelete != null) 
            {
                return Ok(ComposterToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
        
    }
}
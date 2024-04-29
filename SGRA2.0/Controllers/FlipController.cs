using SGRA2._0.Model;
using SGRA2._0.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Threading.ExecutionContext;
using static Azure.Core.HttpHeader;

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlipController : ControllerBase
    {
        private readonly IFlipService _flipService;

        public FlipController(IFlipService flipService)
        {
            _flipService = flipService;
        }

        //GET api/<FlipController>
        [HttpGet]
        public async Task<ActionResult<List<Flip>>> GetAllFlip()
        {
            return Ok(await _flipService.GetAll());
        }

        //GET api/<FlipController>/
        [HttpGet("{IdFlip}")]
        public async Task<ActionResult<Flip>> GetFlip(int IdFlip)
        {
            var flip = await _flipService.GetFlip(IdFlip);
            if (flip == null)
            {
                return BadRequest("Score not found. ");
            }
            return Ok(flip);
        }

        //POST api/<FlipController>
        [HttpPost("Create/")]
        public async Task<ActionResult<Flip>> PostFlip(int IdFlip, int IdWaste, int Flipfrequency, string UniformedDescription)
        {
            var flipToPut = await _flipService.CreateFlip(IdWaste, Flipfrequency, UniformedDescription);
            if (flipToPut != null)
            {
                return Ok(flipToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<FlipController>
        [HttpPut("Update/{IdFlip}")]
        public async Task<ActionResult<FinalCompost>> PutFlip(int IdFlip, int IdWaste, int Flipfrequency, string UniformedDescription)
        {
            var flipToPut = await _flipService.UpdateFlip(IdFlip, IdWaste, Flipfrequency, UniformedDescription);
            if (flipToPut != null)
            {
                return Ok(flipToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
        //DELETE api/<FlipController>
        [HttpPut("Delete/{IdFlip}")]
        public async Task<ActionResult<Flip>> DeleteFlip(int IdFlip)
        {
            var flipToDelete = await _flipService.DeleteFlip(IdFlip);
            if (flipToDelete != null)
            {
                return Ok(flipToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

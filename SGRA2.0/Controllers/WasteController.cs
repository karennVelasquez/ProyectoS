using SGRA2._0.Model;
using SGRA2._0.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Threading.ExecutionContext;
using Microsoft.AspNetCore.Http;
using static Azure.Core.HttpHeader;

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteController : ControllerBase
    {
        private readonly IWasteService _wasteService;
        public WasteController(IWasteService wasteService)
        {
            _wasteService = wasteService;
        }

        //GET api/<WasteController>
        [HttpGet]
        public async Task<ActionResult<Waste>> GetAllWaste()
        {
            return Ok(await _wasteService.GetAll());
        }

        //GET api/<WasteController>/
        [HttpGet("{IdWaste}")]
        public async Task<ActionResult<Waste>> GetWaste(int IdWaste)
        {
            var waste = await _wasteService.GetWaste(IdWaste);
            if(waste == null) 
            {
                return BadRequest("Waste not found. ");
            }
            return Ok(waste);
        }

        //POST api/<WasteController>
        [HttpPost("Create/")]
        public async Task<ActionResult<Waste>> PostWaste(int IdWaste, int IdWasteType, string Humidity)
        {
            var wasteToPut = await _wasteService.CreateWaste(IdWasteType, Humidity);  
            if(wasteToPut != null)
            {
                return Ok(wasteToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<WasteController>
        [HttpPut("Update/{IdWaste}")]
        public async Task<ActionResult<Waste>> PutWaste(int IdWaste, int IdWasteType, string Humidity)
        {
            var wasteToPut = await _wasteService.UpdateWaste(IdWaste, IdWasteType, Humidity);
            if(wasteToPut != null)
            {
                return Ok(wasteToPut);
            }
            else 
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<WasteController>
        [HttpPut("Delete/{IdWaste}")]
        public async Task<ActionResult<Waste>> DeleteWaste(int IdWaste)
        {
            var wasteToDelete = await _wasteService.DeleteWaste(IdWaste);   
            if(wasteToDelete != null) 
            {
                return Ok(wasteToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

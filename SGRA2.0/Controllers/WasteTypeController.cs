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
    public class WasteTypeController : ControllerBase
    {
        private readonly IWasteTypeService _wasteTypeService;
        public WasteTypeController(IWasteTypeService wasteTypeService)
        {
            _wasteTypeService = wasteTypeService;
        }

        //GET api/<WasteTypeController>
        [HttpGet]
        public async Task<ActionResult<List<WasteType>>> GetAllWasteType()
        {
            return Ok(await _wasteTypeService.GetAll());
        }

        //GET api/<WasteTypeController>/
        [HttpGet("{IdWasteType}")]
        public async Task<ActionResult<WasteType>> GetWasteType(int IdWasteType)
        {
            var wasteType = await _wasteTypeService.GetWasteType(IdWasteType);  
            if(wasteType == null) 
            {
                return BadRequest("Waste type noy found. ");
            }
            return Ok(wasteType);
        }

        //POST api/<WasteTypeController>
        [HttpPost("{n}")]
        public async Task<ActionResult<WasteType>> PostWasteType(int IdWasteType, string Waste_Type, string Description, string Descomposition)
        {
            var wasteTypeToPut = _wasteTypeService.CreateWasteType(Waste_Type, Description, Descomposition);    
            if(wasteTypeToPut != null) 
            {
                return Ok(wasteTypeToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<WasteTypeController>
        [HttpPut("Update/{IdWsteType}")]
        public async Task<ActionResult<WasteType>> PutWasteType(int IdWasteType, string Waste_Type, string Description, string Descomposition)
        {
            var wasteTypeToPut = _wasteTypeService.UpdateWasteType(IdWasteType, Waste_Type, Description, Descomposition);
            if(wasteTypeToPut != null) 
            {
                return Ok(wasteTypeToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<WasteTypeController>
        [HttpPut("Delete/{IdWasteType}")]
        public async Task<ActionResult<WasteType>> DeleteWasteType(int IdWasteType)
        {
            var wasteTypeToDelete = await _wasteTypeService.DeleteWasteType(IdWasteType);
            if(wasteTypeToDelete != null)
            {
                return Ok(wasteTypeToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

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
    public class CollectWasteController : ControllerBase
    {
        public readonly ICollectWasteService _collectWasteService;

        public CollectWasteController(ICollectWasteService collectWasteService)
        {
            _collectWasteService = collectWasteService;
        }

        //GET api/<CollectWasteController>
        [HttpGet]
        public async Task<ActionResult<CollectWaste>> GetAllCollectWaste()
        {
            return Ok(await _collectWasteService.GetAll());
        }
        
        //GET api/<CollectWasteController>
        [HttpGet("{IdCollectWaste}")]
        public async Task<ActionResult<CollectWaste>> GetCollectWaste(int IdCollectWaste)
        {
            var CollectWaste = await _collectWasteService.GetCollectWaste(IdCollectWaste);
            if(CollectWaste == null)
            {
                return BadRequest("CollectWaste not found.");
            }
            return Ok(CollectWaste);
        }

        //POST api/<CollectWasteController>
        [HttpPost("Create/")]
        public async Task<ActionResult<CollectWaste>> PostCollectWaste(int IdCollectWaste, int IdSuppliers,int IdComposter, DateTime CollectionDate, int Amount)
        {
            var CollectWasteToPut = await _collectWasteService.CreateCollectWaste(IdSuppliers, IdComposter, CollectionDate, Amount);  
            if(CollectWasteToPut != null)
            {
                return Ok(CollectWasteToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<CollectWasteController>
        [HttpPut("Update/{IdCollectWaste}")]
        public async Task<ActionResult<CollectWaste>> PutCollectWaste(int IdCollectWaste,int IdSuppliers,int IdComposter, DateTime CollectionDate, int Amount)
        {
            var CollectWasteToPut = await _collectWasteService.UpdateCollectWaste(IdCollectWaste, IdSuppliers, IdComposter, CollectionDate, Amount);
            if(CollectWasteToPut != null)
            {
                return Ok(CollectWasteToPut);
            }
            else 
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<CollectWasteController>
        [HttpPut("Delete/{IdCollectWaste}")]
        public async Task<ActionResult<CollectWaste>> DeleteCollectWaste(int IdCollectWaste)
        {
            var CollectWasteToDelete = await _collectWasteService.DeleteCollectWaste(IdCollectWaste);   
            if(CollectWasteToDelete != null) 
            {
                return Ok(CollectWasteToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }        
    }

}
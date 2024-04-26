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
//pruebaaaaaaa
namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ChemicalCompositionController : ControllerBase
    {
        public readonly IChemicalCompositionService _chemicalCompositionService;

        public ChemicalCompositionController(IChemicalCompositionService chemicalCompositionService)
        {
            _chemicalCompositionService = chemicalCompositionService;
        }

        //GET api/<ChemicalcompositionController>
        [HttpGet]
        public async Task<ActionResult<ChemicalComposition>> GetAllChemicalComposition()
        {
            return Ok(await _chemicalCompositionService.GetAll());
        }
        
        //GET api/<ChemicalcompositionController>
        [HttpGet("{IdChemicalComposition")]
        public async Task<ActionResult<ChemicalComposition>> GetChemicalComposition(int IdChemicalComposition)
        {
            var chemicalComposition = await _chemicalCompositionService.GetChemicalComposition(IdChemicalComposition);
            if(chemicalComposition == null)
            {
                return BadRequest("Chemical composition not found.");
            }
            return Ok(chemicalComposition);
        }

        //POST api/<ChemicalcompositionController>
        [HttpPost("{n}")]
        public async Task<ActionResult<ChemicalComposition>> PostChemicalComposition(int IdWaste,string ChemicalComposition)
        {
            var chemicalCompositionToPut = _chemicalCompositionService.CreateChemicalComposition(IdWaste, ChemicalComposition);  
            if(chemicalCompositionToPut != null)
            {
                return Ok(chemicalCompositionToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<ChemicalcompositionController>
        [HttpPut("Update/{IdChemicalComposition}")]
        public async Task<ActionResult<ChemicalComposition>> PutChemicalComposition(int IdChemicalComposition,int IdWaste,string Chemical_Composition)
        {
            var chemicalCompositionToPut = _chemicalCompositionService.UpdateChemicalComposition(IdChemicalComposition,IdWaste, Chemical_Composition);
            if(chemicalCompositionToPut != null)
            {
                return Ok(chemicalCompositionToPut);
            }
            else 
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<ChemicalcompositionController>
        [HttpPut("Delete/{IdChemicalcomposition}")]
        public async Task<ActionResult<ChemicalComposition>> DeleteChemicalComposition(int IdChemicalcomposition)
        {
            var chemicalCompositionToDelete = await _chemicalCompositionService.DeleteChemicalComposition(IdChemicalcomposition);   
            if(chemicalCompositionToDelete != null) 
            {
                return Ok(chemicalCompositionToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}
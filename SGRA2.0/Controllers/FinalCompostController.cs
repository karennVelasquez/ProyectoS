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
    public class FinalCompostController : ControllerBase
    {
        private readonly IFinalCompostService _finalCompostService;

        public FinalCompostController(IFinalCompostService finalCompostService)
        {
            _finalCompostService = finalCompostService;
        }

        //GET api/<FinalCompostController>
        [HttpGet]
        public async Task<ActionResult<List<FinalCompost>>> GetAllFinalCompost()
        {
            return Ok(await _finalCompostService.GetAll());
        }

        //GET api/<FinalCompostController>/
        [HttpGet("{IdFinalCompost}")]
        public async Task<ActionResult<Score>> GetFinalCompost(int IdFinalCompost)
        {
            var finalCompost = await _finalCompostService.GetFinalCompost(IdFinalCompost);
            if (finalCompost == null)
            {
                return BadRequest("Score not found. ");
            }
            return Ok(finalCompost);
        }

        //POST api/<FinalCompostController>
        [HttpPost("{n}")]
        public async Task<ActionResult<Score>> PostFinalCompost(int IdFinalCompost, int IdWaste, string HumidityLevel, string FinalPh, string Nutrients)
        {
            var finalCompostToPut = _finalCompostService.CreateFinalCompost(IdWaste, HumidityLevel, FinalPh, Nutrients);
            if (finalCompostToPut != null)
            {
                return Ok(finalCompostToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<FinalCompostController>
        [HttpPut("Update/{IdFinalCompost}")]
        public async Task<ActionResult<FinalCompost>> PutFinalCompost(int IdFinalCompost, int IdWaste, string HumidityLevel, string FinalPh, string Nutrients)
        {
            var finalCompostToPut = _finalCompostService.UpdateFinalCompost(IdFinalCompost, IdWaste, HumidityLevel, FinalPh, Nutrients);
            if (finalCompostToPut != null)
            {
                return Ok(finalCompostToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
        //DELETE api/<FinalCompostControllerScore>
        [HttpPut("Delete/{IdFinalCompost}")]
        public async Task<ActionResult<FinalCompost>> DeleteFinalCompost(int IdFinalCompost)
        {
            var finalCompostToDelete = await _finalCompostService.DeleteFinalCompost(IdFinalCompost);
            if (finalCompostToDelete != null)
            {
                return Ok(finalCompostToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

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
    public class TimeController : ControllerBase
    {
        private readonly ITimeService _timeService;
        public TimeController(ITimeService timeService) 
        {
            _timeService = timeService;
        }

        //GET api/<TimeController>
        [HttpGet]
        public async Task<ActionResult<List<Time>>> GetAllTime()
        {
            return Ok(await _timeService.GetAll());
        }

        //GET api/<TimeController>/
        [HttpGet("{IdTime}")]
        public async Task<ActionResult<Time>> GetTime(int IdTime)
        {
            var time = await _timeService.GetTime(IdTime);
            if(time == null) 
            {
                return BadRequest("Time not found. ");
            }
            return Ok(time);
        }

        //POST api/<TimeController>
        [HttpPost("Create/")]
        public async Task<ActionResult<Time>> PostTime(int IdTime, int IdWaste, int Processduration, int IdProcessStage)
        {
            var timeToPut = await _timeService.CreateTime(IdWaste, Processduration, IdProcessStage);  
            if(timeToPut != null)
            {
                return Ok(timeToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<TimeController>
        [HttpPut("Update/{IdTime}")]
        public async Task<ActionResult<Time>> PutTime(int IdTime, int IdWaste, int Processduration, int IdProcessStage)
        {
            var timeToPut = await _timeService.UpdateTime(IdTime, IdWaste, Processduration, IdProcessStage);
            if(timeToPut != null)
            {
                return Ok(timeToPut);   
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<TimeController>
        [HttpPut("Delete/{IdTime}")]
        public async Task<ActionResult<Time>> DeleteTime(int IdTime)
        {
            var timeToDelete = await _timeService.DeleteTime(IdTime);
            if(timeToDelete != null) 
            {
                return Ok(timeToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGRA2._0.Model;
using SGRA2._0.Service;

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordTimeController : ControllerBase
    {
        private readonly IRecordTimeService _recordTimeService;
        public RecordTimeController(IRecordTimeService recordTimeService)
        {
            _recordTimeService = recordTimeService;
        }

        //GET api/<RecordTimeController>
        [HttpGet]
        public async Task<ActionResult<List<RecordTime>>> GetAllRecordTime()
        {
            return Ok(await _recordTimeService.GetAll());
        }

        //GET api/<RecordTimeController>/
        [HttpGet("{IdRecordTime}")]
        public async Task<ActionResult<RecordTime>> GetRecordTime(int IdRecordTime)
        {
            var recordTime = await _recordTimeService.GetRecordTime(IdRecordTime);
            if (recordTime == null)
            {
                return BadRequest("Waste type not found. ");
            }
            return Ok(recordTime);
        }

        //POST api/<RecordTimeController>
        [HttpPost("{n}")]
        public async Task<ActionResult<RecordTime>> PostRecordTime(int IdRecordTime, int IdLevel, int IdWaste, DateTime Collecttime, int AmountCollected)
        {
            var recordTimeToPut = _recordTimeService.CreateRecordTime(IdLevel, IdWaste, Collecttime, AmountCollected);
            if (recordTimeToPut != null)
            {
                return Ok(recordTimeToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<RecordTimeController>
        [HttpPut("Update/{IdRecordTime}")]
        public async Task<ActionResult<RecordTime>> PutRecordTime(int IdRecordTime, int IdLevel, int IdWaste, DateTime Collecttime, int AmountCollected)
        {
            var recordTimeToPut = _recordTimeService.UpdateRecordTime(IdRecordTime, IdLevel, IdWaste, Collecttime, AmountCollected);
            if (recordTimeToPut != null)
            {
                return Ok(recordTimeToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<RecordTimeController>
        [HttpPut("Delete/{IdRecordTime}")]
        public async Task<ActionResult<RecordTime>> DeleteRecordTime(int IdRecordTime)
        {
            var recordTimeToDelete = await _recordTimeService.DeleteRecordTime(IdRecordTime);
            if (recordTimeToDelete != null)
            {
                return Ok(recordTimeToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

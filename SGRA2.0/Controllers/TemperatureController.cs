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
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureService _temperatureService;

        public TemperatureController(ITemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }

        //GET api/<TemperatureController>
        [HttpGet]
        public async Task<ActionResult<List<Temperature>>> GetAllTemperature()
        {
            return Ok(await _temperatureService.GetAll());
        }

        //GET api/<temperatureController>/
        [HttpGet("{IdTemperature}")]
        public async Task<ActionResult<Temperature>> GetTemperature(int IdTemperature)
        {
            var temperature = await _temperatureService.GetTemperature(IdTemperature);
            if (temperature == null)
            {
                return BadRequest("Temperature not found. ");
            }
            return Ok(temperature);
        }

        //POST api/<temperatureController>
        [HttpPost("Create/")]
        public async Task<ActionResult<Temperature>> PostTemperature(int IdTemperature, int IdWaste, string Decompositiontemperature)
        {
            var temperatureToPut = await _temperatureService.CreateTemperature(IdWaste, Decompositiontemperature);
            if (temperatureToPut != null)
            {
                return Ok(temperatureToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<temperatureController>
        [HttpPut("Update/{IdTemperature}")]
        public async Task<ActionResult<Temperature>> PutTemperature(int IdTemperature, int IdWaste, string Decompositiontemperature)
        {
            var temperatureToPut = await _temperatureService.UpdateTemperature(IdTemperature, IdWaste, Decompositiontemperature);
            if (temperatureToPut != null)
            {
                return Ok(temperatureToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<temperatureController>
        [HttpPut("/Delte/{IdTemperature}")]
        public async Task<ActionResult<Temperature>> DeleteTemperature(int IdTemperature)
        {
            var temperatuteToDelete = await _temperatureService.DeleteTemperature(IdTemperature);
            if (temperatuteToDelete != null)
            {
                return Ok(temperatuteToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

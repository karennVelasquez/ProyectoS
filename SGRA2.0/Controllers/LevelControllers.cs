using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGRA2._0.Model;
using SGRA2._0.Service;

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelControllers : ControllerBase
    {
        private readonly ILevelService _levelService;
        public LevelControllers(ILevelService levelService)
        {
            _levelService = levelService;
        }

        //GET api/<WasteTypeController>
        [HttpGet]
        public async Task<ActionResult<List<Level>>> GetAllLevel()
        {
            return Ok(await _levelService.GetAll());
        }

        //GET api/<WasteTypeController>/
        [HttpGet("{IdLevel}")]
        public async Task<ActionResult<Level>> GetLevel(int IdLevel)
        {
            var level = await _levelService.GetLevel(IdLevel);
            if (level == null)
            {
                return BadRequest("Waste type noy found. ");
            }
            return Ok(level);
        }

        //POST api/<WasteTypeController>
        [HttpPost("{n}")]
        public async Task<ActionResult<Level>> PostLevel(int IdLevel, int NumLevel)
        {
            var levelToPut = _levelService.CreateLevel(IdLevel, NumLevel);
            if (levelToPut != null)
            {
                return Ok(levelToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<WasteTypeController>
        [HttpPut("Update/{IdLevel}")]
        public async Task<ActionResult<Level>> PutLevel(int IdLevel, int NumLevel)
        {
            var levelToPut = _levelService.UpdateLevel(IdLevel, NumLevel);
            if (levelToPut != null)
            {
                return Ok(levelToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<WasteTypeController>
        [HttpPut("Delete/{IdLevel}")]
        public async Task<ActionResult<Level>> DeleteLevel(int IdLevel)
        {
            var levelToDelete = await _levelService.DeleteLevel(IdLevel);
            if (levelToDelete != null)
            {
                return Ok(levelToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

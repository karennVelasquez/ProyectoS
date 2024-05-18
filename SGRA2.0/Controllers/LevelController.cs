using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGRA2._0.Model;
using SGRA2._0.Service;

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelService;
        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }

        //GET api/<LevelController>
        [HttpGet]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<List<Level>>> GetAllLevel()
        {
            return Ok(await _levelService.GetAll());
        }

        //GET api/<LevelController>
        [HttpGet("{IdLevel}")]
       // [Authorize(Roles = "User")]
        public async Task<ActionResult<Level>> GetLevel(int IdLevel)
        {
            var level = await _levelService.GetLevel(IdLevel);
            if (level == null)
            {
                return BadRequest("Waste type not found. ");
            }
            return Ok(level);
        }

        //POST api/<LevelController>
        [HttpPost("Create/")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<Level>> PostLevel(int IdLevel, int NumLevel)
        {
            var levelToPut = await _levelService.CreateLevel( NumLevel);
            if (levelToPut != null)
            {
                return Ok(levelToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<LevelController>
        [HttpPut("Update/{IdLevel}")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<Level>> PutLevel(int IdLevel, int NumLevel)
        {
            var levelToPut = await _levelService.UpdateLevel(IdLevel, NumLevel);
            if (levelToPut != null)
            {
                return Ok(levelToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<LevelController>
        [HttpPut("Delete/{IdLevel}")]
        //[Authorize(Roles = "User")]
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

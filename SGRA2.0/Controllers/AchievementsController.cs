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

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AchievementsController : ControllerBase
    {
        public readonly IAchievementsService _achievementsService;

        public AchievementsController(IAchievementsService achievementsService)
        {
            _achievementsService = achievementsService;
        }

        //GET api/<AchievementsController>
        [HttpGet]
        public async Task<ActionResult<Achievements>> GetAllAchievement()
        {
            return Ok(await _achievementsService.GetAll());
        }
        
        //GET api/<AchievementsController>
        [HttpGet("{IdAchievements}")]
        public async Task<ActionResult<Achievements>> GetAchievements(int IdAchievements)
        {
            var achievements = await _achievementsService.GetAchievements(IdAchievements);
            if(achievements == null)
            {
                return BadRequest("Achievement not found.");
            }
            return Ok(achievements);
        }

        //POST api/<AchievementsController>
        [HttpPost("Create/")]
        public async Task<ActionResult<Achievements>> PostAchievement(int IdAchievements,int IdUser, int IdGames)
        {
            var achievementsToPut = await _achievementsService.CreateAchievements(IdUser, IdGames);  
            if(achievementsToPut != null)
            {
                return Ok(achievementsToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<AchievementsController>
        [HttpPut("Update/{IdAchievements}")]
        public async Task<ActionResult<Achievements>> PutAchievement(int IdAchievements, int IdUser, int IdGames)
        {
            var achievementsToPut = await _achievementsService.UpdateAchievements(IdAchievements, IdUser, IdGames);
            if(achievementsToPut != null)
            {
                return Ok(achievementsToPut);
            }
            else 
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<AchievementsController>
        [HttpPut("Delete/{IdAchievements}")]
        public async Task<ActionResult<Achievements>> DeleteAchievement(int IdAchievements)
        {
            var achievementsToDelete = await _achievementsService.DeleteAchievements(IdAchievements);   
            if(achievementsToDelete != null) 
            {
                return Ok(achievementsToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}
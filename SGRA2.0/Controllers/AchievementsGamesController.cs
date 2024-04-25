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
    public class AchievementsGamesController : ControllerBase
    {
        public readonly IAchievementsGamesService _achievementsGamesService;

        public AchievementsGamesController(IAchievementsGamesService achievementsGamesService)
        {
            _achievementsGamesService = achievementsGamesService;
        }

        //GET api/<AchievementsGamesController>
        [HttpGet]
        public async Task<ActionResult<AchievementsGames>> GetAllAchievementsGames()
        {
            return Ok(await _achievementsGamesService.GetAll());
        }

        //GET api/<AchievementsController>
        [HttpGet("{IdAchievementsG}")]
        public async Task<ActionResult<AchievementsGames>> GetAchievementsGames(int IdAchievementsG)
        {
            var achievementsGames = await _achievementsGamesService.GetAchievementsGames(IdAchievementsG);
            if(achievementsGames == null)
            {
                return BadRequest("Achievement not found.");
            }
            return Ok(achievementsGames);
        }

        //POST api/<AchievementsController>
        [HttpPost("{n}")]
        public async Task<ActionResult<AchievementsGames>> PostAchievementGames(int IdAchievementsG,int IdGames,int IdAchievements)
        {
            var achievementsGamesToPut = _achievementsGamesService.CreateAchievementsGames(IdGames,IdAchievements);  
            if(achievementsGamesToPut != null)
            {
                return Ok(achievementsGamesToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<AchievementsController>
        [HttpPut("Update/{IdAchievements}")]
        public async Task<ActionResult<AchievementsGames>> PutAchievementGames(int IdAchievementsG,int IdGames,int IdAchievements)
        {
            var achievementsGamesToPut = _achievementsGamesService.UpdateAchievementsGames(IdAchievementsG,IdGames,IdAchievements);
            if(achievementsGamesToPut != null)
            {
                return Ok(achievementsGamesToPut);
            }
            else 
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<AchievementsController>
        [HttpPut("Delete/{IdAchievements}")]
        public async Task<ActionResult<AchievementsGames>> DeleteAchievementGames(int IdAchievementsG)
        {
            var achievementsGamesToDelete = await _achievementsGamesService.DeleteAchievementsGames(IdAchievementsG);   
            if(achievementsGamesToDelete != null) 
            {
                return Ok(achievementsGamesToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
        
    }
}
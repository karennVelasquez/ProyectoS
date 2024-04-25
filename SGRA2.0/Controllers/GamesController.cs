using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGRA2._0.Model;
using SGRA2._0.Service;

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;
        public GamesController(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }

        //GET api/<GamesController>
        [HttpGet]
        public async Task<ActionResult<List<Games>>> GetAllGames()
        {
            return Ok(await _gamesService.GetAll());
        }

        //GET api/<GamesController>/
        [HttpGet("{IdGames}")]
        public async Task<ActionResult<Games>> GetGames(int IdGames)
        {
            var games = await _gamesService.GetGames(IdGames);
            if (games == null)
            {
                return BadRequest("Waste type not found. ");
            }
            return Ok(games);
        }

        //POST api/<GamesController>
        [HttpPost("{n}")]
        public async Task<ActionResult<Games>> PostGame(int IdGames, int IdUser, int IdLevel, DateTime StartDate, DateTime FinalDate)
        {
            var gamesToPut = _gamesService.CreateGames(IdUser, IdLevel, StartDate, FinalDate);
            if (gamesToPut != null)
            {
                return Ok(gamesToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<GamesController>
        [HttpPut("Update/{IdGames}")]
        public async Task<ActionResult<Games>> PutGame(int IdGames, int IdUser, int IdLevel, DateTime StartDate, DateTime FinalDate)
        {
            var gamesToPut = _gamesService.UpdateGames(IdGames, IdUser, IdLevel, StartDate, FinalDate);
            if (gamesToPut != null)
            {
                return Ok(gamesToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<GamesController>
        [HttpPut("Delete/{IdGames}")]
        public async Task<ActionResult<Games>> DeleteGames(int IdGames)
        {
            var gamesToDelete = await _gamesService.DeleteGames(IdGames);
            if (gamesToDelete != null)
            {
                return Ok(gamesToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

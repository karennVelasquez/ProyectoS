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
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _scoreService;

        public ScoreController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        //GET api/<ScoreController>
        [HttpGet]
        public async Task<ActionResult<List<Score>>> GetAllScore()
        {
            return Ok(await _scoreService.GetAll());
        }

        //GET api/<ScoreController>/
        [HttpGet("{IdScore}")]
        public async Task<ActionResult<Score>>GetScore(int IdScore)
        {
            var score = await _scoreService.GetScore(IdScore);
            if(score == null) 
            {
                return BadRequest("Score not found. ");
            }
            return Ok(score);
        }

        //POST api/<Score>
        [HttpPost("Create/")]
        public async Task<ActionResult<Score>> PostScore(int IdScore, int IdUser, int IdGames, int NumScore)
        {
            var scoreToPut = await _scoreService.CreateScore(IdUser, IdGames, NumScore);
            if(scoreToPut != null) 
            {
                return Ok(scoreToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<Score>
        [HttpPut("Update/{IdScore}")]
        public async Task<ActionResult<Score>> PutScore(int IdScore, int IdUser, int IdGames, int NumScore)
        {
            var scoreToPut = await _scoreService.UpdateScore(IdScore, IdUser, IdGames, NumScore);
            if(scoreToPut != null) 
            {
                return Ok(scoreToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<Score>
        [HttpPut("Delete/{IdScore}")]
        public async Task<ActionResult<Score>> DeleteScore(int IdScore)
        {
            var scoreToDelete = await _scoreService.DeleteScore(IdScore);
            if( scoreToDelete != null)
            {
                return Ok(scoreToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }

}

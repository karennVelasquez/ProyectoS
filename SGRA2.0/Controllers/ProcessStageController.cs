using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGRA2._0.Model;
using SGRA2._0.Service;

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessStageController : ControllerBase
    {
        private readonly IProcessStageService _processStageService;
        public ProcessStageController(IProcessStageService processStageService)
        {
            _processStageService = processStageService;
        }

        //GET api/<ProcessStageController>
        [HttpGet]
        public async Task<ActionResult<List<ProcessStage>>> GetAllProcessStage()
        {
            return Ok(await _processStageService.GetAll());
        }

        //GET api/<WasteTypeController>/
        [HttpGet("{IdProcessStage}")]
        public async Task<ActionResult<ProcessStage>> GetProcessStage(int IdProcessStage)
        {
            var processStage = await _processStageService.GetProcessStage(IdProcessStage);
            if (processStage == null)
            {
                return BadRequest("Waste type not found. ");
            }
            return Ok(processStage);
        }

        //POST api/<ProcessStageController>
        [HttpPost("Create/")]
        public async Task<ActionResult<ProcessStage>> PostProcessStage(int IdProcessStage, string Stage)
        {
            var processStageToPut = await _processStageService.CreateProcessStage(Stage);
            if (processStageToPut != null)
            {
                return Ok(processStageToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<ProcessStageController>
        [HttpPut("Update/{IdProcessStage}")]
        public async Task<ActionResult<ProcessStage>> PutProcessStage(int IdProcessStage, string Stage)
        {
            var processStageToPut = await _processStageService.UpdateProcessStage(IdProcessStage, Stage);
            if (processStageToPut != null)
            {
                return Ok(processStageToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<ProcessStageController>
        [HttpPut("Delete/{IdProcessStage}")]
        public async Task<ActionResult<ProcessStage>> DeleteProcessStage(int IdProcessStage)
        {
            var processStageToDelete = await _processStageService.DeleteProcessStage(IdProcessStage);
            if (processStageToDelete != null)
            {
                return Ok(processStageToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

using SGRA2._0.Model;
using SGRA2._0.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Threading.ExecutionContext;
using Microsoft.AspNetCore.Http;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonLoginController : ControllerBase
    {
        private readonly IPersonLoginService _personLoginService;
        public PersonLoginController(IPersonLoginService personLoginService)
        {
            _personLoginService = personLoginService;
        }

        //GET api/<PersonLogin>
        [HttpGet]
       // [Authorize(Roles = "Person")]
        public async Task<ActionResult<List<PersonLogin>>> GetAllPersonLogin()
        {
            return Ok(await _personLoginService.GetAll());
        }

        //GET api/<PersonLogin>Id
        [HttpGet("{IdLoginP}")]
        //[Authorize(Roles = "Person")]
        public async Task<ActionResult<PersonLogin>> GetPersonLogin( int IdLoginP)
        {
            var personLogin = await _personLoginService.GetPersonLogin(IdLoginP);
            if(personLogin == null) 
            {
                return BadRequest(" User not found. ");
            }
            return Ok(personLogin);
        }

        //GET api/BYUSERNAME
        [HttpGet("IdByUsername/{username}")]
        public async Task<IActionResult> GetIdByUsernameP(string usernameP)
        {
            var id = await _personLoginService.GetIdByUsernameP(usernameP);
            if (id == null)
            {
                return NotFound();
            }
            return Ok(id);
        }

        //POST api/<PersonLogin>
        [HttpPost("Create/")]
       // [Authorize(Roles = "Person")]
        public async Task<ActionResult<PersonLogin>> PostPersonLogin(int IdLoginP, string UserName, string Password, int IdPerson)
        {
            var personLoginToPut = await _personLoginService.CreatePersonLogin(UserName, Password, IdPerson);
            if( personLoginToPut != null)
            {
                return Ok(personLoginToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //AUTENTICACION
        [HttpPost("Authentication")]
        public async Task<ActionResult<string>> LoginP(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Name and password are required. ");
            }
            // bool user = await _userService.Authentication(userName, email, password);
            bool userp = await _personLoginService.Authentication(userName, password);
            if (userp != null)
            {
                string token = _personLoginService.GenerarToken(userName);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }

        //PUT api/<LoginPerson>
        [HttpPut("Update/{IdLoginP}")]
        public async Task<ActionResult<PersonLogin>> PutPersonLogin (int IdLoginP, string UserName, string Password, int IdPerson)
        {
            var PersonLoginToPut = await _personLoginService.UpdatePersonLogin(IdLoginP, UserName, Password, IdPerson);
            if (PersonLoginToPut != null) 
            {
                return Ok(PersonLoginToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

    }
}

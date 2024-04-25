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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        //GET api/<UserController>
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {
            return Ok(await _userService.GetAll());
        }

        //GET api/<UserController>/
        [HttpGet("{IdUser}")]
        public async Task<ActionResult<User>> GetUser(int IdUser)
        {
            var user = await _userService.GetUser(IdUser);
            if(user == null) 
            {
                return BadRequest("User not found. ");
            }
            return Ok(user);
        }

        //POST api/<UserController>
        [HttpPost("{n}")]
        public async Task<ActionResult<User>> PostUser(int IdUser, string UserName, string Password)
        {
            var userToPut = _userService.CreateUser(UserName, Password);
            if(userToPut != null) 
            {
                return Ok(userToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<UserController>
        [HttpPut("Update/{IdUser}")]
        public async Task<ActionResult<User>> PutUser(int IdUser, string UserName, string Password)
        {
            var userToPut = _userService.UpdateUser(IdUser, UserName, Password);    
            if( userToPut != null )
            {
                return Ok(userToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<UserController>
        [HttpPut("/Delete/{IdUser}")]
        public async Task<ActionResult<User>> DeleteUser(int IdUser)
        {
            var userToDelete = await _userService.DeleteUser(IdUser);   
            if(userToDelete != null) 
            {
                return Ok(userToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

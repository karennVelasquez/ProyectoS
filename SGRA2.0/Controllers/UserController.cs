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
        [HttpPost("Create/")]
        public async Task<ActionResult<User>> PostUser(int IdUser, string UserName, string Email, string Password)
        {
            var userToPut = await _userService.CreateUser(UserName, Email, Password);
            if(userToPut != null) 
            {
                return Ok(userToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //AUTENTICACION
        [HttpPost("Login")]
        public async Task<ActionResult<bool>> Login(string userName, string email, string password)
        {
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) 
            {
                return BadRequest("Name, email and password are required. ");
            } 
           
            var user = await _userService.Login(userName, email, password);
            if(user == null) 
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        //PUT api/<UserController>
        [HttpPut("Update/{IdUser}")]
        public async Task<ActionResult<User>> PutUser(int IdUser, string UserName, string Password)
        {
            var userToPut = await _userService.UpdateUser(IdUser, UserName, Password);    
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
        [HttpPut("Delete/{IdUser}")]
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

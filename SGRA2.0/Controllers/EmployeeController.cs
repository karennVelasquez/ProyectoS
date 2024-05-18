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
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //GET api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<Employee>> GetAllEmployee()
        {
            return Ok(await _employeeService.GetAll());
        }
        
        //GET api/<EmployeeController>
        [HttpGet("{IdEmployee}")]
        public async Task<ActionResult<Employee>> GetEmployee(int IdEmployee)
        {
            var Employee = await _employeeService.GetEmployee(IdEmployee);
            if(Employee == null)
            {
                return BadRequest("Employee not found.");
            }
            return Ok(Employee);
        }

        //POST api/<EmployeeController>
        [HttpPost("Create/")]
        public async Task<ActionResult<Employee>> PostEmployee(int IdEmployee, int IdPerson, string Position)
        {
            var EmployeeToPut = await _employeeService.CreateEmployee(IdPerson, Position);  
            if(EmployeeToPut != null)
            {
                return Ok(EmployeeToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<EmployeeController>
        [HttpPut("Update/{IdEmployee}")]
        public async Task<ActionResult<Employee>> PutEmployee(int IdEmployee, int IdPerson, string Position)
        {
            var EmployeeToPut = await _employeeService.UpdateEmployee(IdEmployee, IdPerson, Position);
            if(EmployeeToPut != null)
            {
                return Ok(EmployeeToPut);
            }
            else 
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<EmployeeController>
        [HttpPut("Delete/{IdEmployee}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int IdEmployee)
        {
            var EmployeeToDelete = await _employeeService.DeleteEmployee(IdEmployee);   
            if(EmployeeToDelete != null) 
            {
                return Ok(EmployeeToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}
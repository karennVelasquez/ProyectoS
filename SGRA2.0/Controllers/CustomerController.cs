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
    public class CustomerController : ControllerBase
    {
        public readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        //GET api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<Customer>> GetAllCustomer()
        {
            return Ok(await _customerService.GetAll());
        }
        
        //GET api/<CustomerController>
        [HttpGet("{IdCustomer}")]
        public async Task<ActionResult<Customer>> GetCustomer(int IdCustomer)
        {
            var Customer = await _customerService.GetCustomer(IdCustomer);
            if(Customer == null)
            {
                return BadRequest("Customer not found.");
            }
            return Ok(Customer);
        }

        //POST api/<CustomerController>
        [HttpPost("Create/")]
        public async Task<ActionResult<Customer>> PostCustomer(int IdPerson)
        {
            var CustomerToPut = await _customerService.CreateCustomer(IdPerson);  
            if(CustomerToPut != null)
            {
                return Ok(CustomerToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<CustomerController>
        [HttpPut("Update/{IdCustomer}")]
        public async Task<ActionResult<Customer>> PutCustomer(int IdCustomer, int IdPerson)
        {
            var CustomerToPut = await _customerService.UpdateCustomer(IdCustomer,IdPerson);
            if(CustomerToPut != null)
            {
                return Ok(CustomerToPut);
            }
            else 
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<CustomerController>
        [HttpPut("Delete/{IdCustomer}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int IdCustomer)
        {
            var CustomerToDelete = await _customerService.DeleteCustomer(IdCustomer);   
            if(CustomerToDelete != null) 
            {
                return Ok(CustomerToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
        
    }
}
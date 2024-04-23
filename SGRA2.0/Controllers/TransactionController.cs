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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;   
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        //GET api/<TransactionController>
        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetAllTransaction()
        {
            return Ok(await _transactionService.GetAll());
        }

        //GET api/<TransactionController>/
        [HttpGet("{IdTransaction}")]
        public async Task<ActionResult<Transaction>> GetTransaction (int IdTransaction)
        {
            var trasanction = await _transactionService.GetTransaction(IdTransaction);  
            if(trasanction == null) 
            {
                return BadRequest("Error not found. ");
            }
            return Ok(trasanction);
        }

        //POST api/<TransactionController>
        [HttpPost("{n}")]
        public async Task<ActionResult<Transaction>> PostTransaction(int IdTransaction, int IdSuppliers, int DeliveredQuantity, DateTime DeliveredDate, string Price, string Quality)
        {
            var transactionToPut = _transactionService.CreateTransaction(IdSuppliers, DeliveredQuantity, DeliveredDate, Price, Quality);
            if(transactionToPut != null) 
            {
                return Ok(transactionToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<TransactionController>
        [HttpPut("Update/{IdTransaction}")]
        public async Task<ActionResult<Transaction>> PutTransaction(int IdTransaction, int IdSuppliers, int DeliveredQuantity, DateTime DeliveredDate, string Price, string Quality)
        {
            var transactionToPut = _transactionService.CreateTransaction(IdSuppliers, DeliveredQuantity, DeliveredDate, Price, Quality);
            if(transactionToPut != null) 
            {
                return Ok(transactionToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<TransactionController>
        [HttpPut("/Delete{IdTransaction}")]
        public async Task<ActionResult<Transaction>> DeleteTransaction(int IdTransaction)
        {
            var transactionToDelete = await _transactionService.DeleteTransaction(IdTransaction);
            if( transactionToDelete != null )
            {
                return Ok(transactionToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

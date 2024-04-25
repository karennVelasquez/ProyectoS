using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGRA2._0.Model;
using SGRA2._0.Service;

namespace SGRA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        //GET api/<SaleController>
        [HttpGet]
        public async Task<ActionResult<List<Sale>>> GetAllSale()
        {
            return Ok(await _saleService.GetAll());
        }

        //GET api/<SaleController>/
        [HttpGet("{IdSale}")]
        public async Task<ActionResult<Sale>> GetSale(int IdSale)
        {
            var sale = await _saleService.GetSale(IdSale);
            if (sale == null)
            {
                return BadRequest("Waste type not found. ");
            }
            return Ok(sale);
        }

        //POST api/<SaleController>
        [HttpPost("{n}")]
        public async Task<ActionResult<Sale>> PostSale(int IdSale, int IdCustomer, DateTime SaleDate)
        {
            var saleToPut = _saleService.CreateSale( IdCustomer, SaleDate);
            if (saleToPut != null)
            {
                return Ok(saleToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<SaleController>
        [HttpPut("Update/{IdSale}")]
        public async Task<ActionResult<Sale>> PutSale(int IdSale, int IdCustomer, DateTime SaleDate)
        {
            var saleToPut = _saleService.UpdateSale(IdSale, IdCustomer, SaleDate);
            if (saleToPut != null)
            {
                return Ok(saleToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<SaleController>
        [HttpPut("Delete/{IdSale}")]
        public async Task<ActionResult<Sale>> DeleteSale(int IdSale)
        {
            var saleToDelete = await _saleService.DeleteSale(IdSale);
            if (saleToDelete != null)
            {
                return Ok(saleToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

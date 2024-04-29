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
    public class SuppliersController : ControllerBase
    { 
        private readonly ISuppliersService _suppliersService;
        public SuppliersController(ISuppliersService suppliersService)
        {
            _suppliersService = suppliersService;
        }

        //GET api/<SuppliersController>
        [HttpGet]
        public async Task<ActionResult<List<Suppliers>>> GetAllSuppliers()
        {
            return Ok(await _suppliersService.GetAll());
        }

        //GET api/<SupplierController>
        [HttpGet("{IdSuppliers}")]
        public async Task<ActionResult<Suppliers>> GetSuppliers(int IdSuppliers)
        {
            var suppliers = await _suppliersService.GetSuppliers(IdSuppliers);
            if(suppliers == null) 
            {
                return BadRequest("Suppliers not found. ");
            }
            return Ok(suppliers);
        }

        //POST api/<Suppliers>
        [HttpPost("Create/")]
        public async Task<ActionResult<Suppliers>> PostSuppliers(int IdSuppliers, int IdPerson, int IdWasteType)
        {
            var suppliersToPut = await _suppliersService.CreateSuppliers(IdPerson, IdWasteType);
            if(suppliersToPut != null)
            {
                return Ok(suppliersToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<Suppliers>
        [HttpPut("Update/{IdSuppliers}")]
        public async Task<ActionResult<Suppliers>> PutSuppliers(int IdSuppliers, int IdPerson, int IdWasteType)
        {
            var SuppliersToPut = await _suppliersService.UpdateSuppliers(IdSuppliers,IdPerson, IdWasteType); 
            if(SuppliersToPut != null) 
            {
                return Ok(SuppliersToPut);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
           

        //DELETE api/<Suppliers>
        [HttpPut("Delete/{IdSuppliers}")]
        public async Task<ActionResult<Suppliers>> DeleteSuppliers(int IdSuppliers)
        {
            var suppliersToDelete = await _suppliersService.DeleteSuppliers(IdSuppliers);
            if(suppliersToDelete != null) 
            {
                return Ok(suppliersToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}

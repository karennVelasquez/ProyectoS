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
    public class DocumentTypeController : ControllerBase
    {
        public readonly IDocumentTypeService _documentTypeService;

        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }

        //GET api/<DocumentTypesController>
        [HttpGet]
        public async Task<ActionResult<DocumentType>> GetAllDocumentType()
        {
            return Ok(await _documentTypeService.GetAll());
        }
        
        //GET api/<DocumentTypesController>
        [HttpGet("{IdDocumentTypes}")]
        public async Task<ActionResult<DocumentType>> GetDocumentType(int IdDocumentTypes)
        {
            var documentType = await _documentTypeService.GetDocumentType(IdDocumentTypes);
            if(documentType == null)
            {
                return BadRequest("DocumentType not found.");
            }
            return Ok(documentType);
        }

        //POST api/<DocumentTypesController>
        [HttpPost("Create/")]
        public async Task<ActionResult<DocumentType>> PostDocumentType(int IdDocumentTypes, string Document)
        {
            var DocumentTypesToPut = await _documentTypeService.CreateDocumentType(Document);  
            if(DocumentTypesToPut != null)
            {
                return Ok(DocumentTypesToPut);
            }
            else
            {
                return BadRequest("Error when inserting into the database. ");
            }
        }

        //PUT api/<DocumentTypesController>
        [HttpPut("Update/{IdDocumentType}")]
        public async Task<ActionResult<DocumentType>> PutDocumentType(int IdDocumentType, string Document)
        {
            var DocumentTypesToPut = await _documentTypeService.UpdateDocumentType(IdDocumentType, Document);
            if(DocumentTypesToPut != null)
            {
                return Ok(DocumentTypesToPut);
            }
            else 
            {
                return BadRequest("Error updating the database. ");
            }
        }

        //DELETE api/<DocumentTypesController>
        [HttpPut("Delete/{IdDocumentType}")]
        public async Task<ActionResult<DocumentType>> DeleteDocumentType(int IdDocumentType)
        {
            var documentTypeToDelete = await _documentTypeService.DeleteDocumentType(IdDocumentType);   
            if(documentTypeToDelete != null) 
            {
                return Ok(documentTypeToDelete);
            }
            else
            {
                return BadRequest("Error updating the database. ");
            }
        }
    }
}
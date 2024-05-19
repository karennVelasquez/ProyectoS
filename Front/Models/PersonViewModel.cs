using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Front.Models
{
    public class PersonViewModel
    {
        public PersonViewModel() 
        {
            TypesDocument = new List<SelectListItem>();
        }

        [DisplayName("Id")]
        public int IdPerson { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        
        [DisplayName("Document Type")]
        public int IdDocumentType { get; set; }
        public IEnumerable<SelectListItem> TypesDocument { get; set; }
        public string Document { get; set; } // // Propiedad para mostrar el nombre del tipo de identificación


        [DisplayName("Number Document")]
        public int NumDocument { get; set; }
        public bool IsDelete { get; set; }
    }
}

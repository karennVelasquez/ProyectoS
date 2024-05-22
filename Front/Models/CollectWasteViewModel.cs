using SGRA2._0.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SGRA2._0.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Front.Models
{
    public class CollectWasteViewModel
    {
        public CollectWasteViewModel()
        {
            Suppliers = new List<SelectListItem>();
            Person = new List<SelectListItem>();
            DocumentType = new List<SelectListItem>();
            Composter = new List<SelectListItem>();
        }
        [DisplayName("Id")]
        public int IdCollectWaste { get; set; }


        public int IdSuppliers { get; set; }
        public IEnumerable<SelectListItem> Suppliers { get; set; }
        public int IdPerson { get; set; }
        public IEnumerable<SelectListItem> Person { get; set; }
        [DisplayName("Suppliers Name")]
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int IdDocumentType { get; set; }
        public IEnumerable<SelectListItem> DocumentType { get; set; }
        [DisplayName("Document Type")]
        public string Document { get; set; } // // Propiedad para mostrar el nombre del tipo de identificación

        [DisplayName("Number Document")]
        public int NumDocument { get; set; }


        [DisplayName("Composter")]
        public int IdComposter { get; set; }

        public IEnumerable<SelectListItem> Composter { get; set; }
        public string Material { get; set; }
        public string DrainageSystem { get; set; }


        public DateTime CollectionDate { get; set; }
        public int Amount { get; set; }
        public bool IsDelete { get; set; }
    }
}
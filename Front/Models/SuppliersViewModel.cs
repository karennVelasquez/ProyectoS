using SGRA2._0.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SGRA2._0.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Front.Models
{
    public class SuppliersViewModel
    {
        public SuppliersViewModel()
        {
            Person = new List<SelectListItem>();
            DocumentType = new List<SelectListItem>();
            WasteType = new List<SelectListItem>();
        }
        [DisplayName("Id")]
        public int IdSuppliers { get; set; }

        public int IdPerson { get; set; }
        public IEnumerable<SelectListItem> Person { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public int IdDocumentType { get; set; }
        public IEnumerable<SelectListItem> DocumentType { get; set; }
        [DisplayName("Document Type")]
        public string Document { get; set; }


        [DisplayName("Number Document")]
        public int NumDocument { get; set; }


        public int IdWasteType { get; set; }
        public IEnumerable<SelectListItem> WasteType { get; set; }
        [DisplayName("Waste Type")]
        public string Waste_Type { get; set; }
        public bool IsDelete { get; set; }
    }
}

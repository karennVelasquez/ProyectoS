using Microsoft.AspNetCore.Mvc.Rendering;
using SGRA2._0.Model;
using System.ComponentModel;

namespace Front.Models
{
    public class EmployeeViewModel
    {
        [DisplayName("Id")]
        public int IdEmployee { get; set; }
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

        public string Position { get; set; }
        public bool IsDelete { get; set; }
    }
}

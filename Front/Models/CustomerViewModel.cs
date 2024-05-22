using Microsoft.AspNetCore.Mvc.Rendering;
using SGRA2._0.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            Person = new List<SelectListItem>();
            DocumentTypes = new List<SelectListItem>();
        }

            [DisplayName("Id")]
            public int IdCustomer { get; set; }
            public int IdPerson { get; set; }
            public IEnumerable<SelectListItem> Person { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }

            public int IdDocumentType { get; set; }
            public IEnumerable<SelectListItem> DocumentTypes { get; set; }
            [DisplayName("Document Type")]
            public string Document {  get; set; }


            [DisplayName("Number Document")]
            public int NumDocument { get; set; }
            public bool IsDelete { get; set; }
    }
}



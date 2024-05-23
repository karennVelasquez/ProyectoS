using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGRA2._0.Model;

namespace Front.Models
{
    public class TransactionViewModel
    {
        public TransactionViewModel()
        {
            Suppliers = new List<SelectListItem>();
            Person = new List<SelectListItem>();
            DocumentTypes = new List<SelectListItem>();
        }

        [DisplayName("Id")]
        public int IdTransaction { get; set; }

        public int IdSuppliers { get; set; }
        public IEnumerable<SelectListItem> Suppliers { get; set; }
        public int IdPerson { get; set; }
        public IEnumerable<SelectListItem> Person { get; set; }
        [DisplayName("Suppliers Name")]
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public int IdDocumentType { get; set; }
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
        [DisplayName("Document Type")]
        public string Document { get; set; } // // Propiedad para mostrar el nombre del tipo de identificación

        [DisplayName("Number Document")]
        public int NumDocument { get; set; }



        public int DeliveredQuantity { get; set; }
        public DateTime DeliveredDate { get; set; }
        public string Price { get; set; }
        public string Quality { get; set; }

        public bool IsDelete { get; set; }
    }
}
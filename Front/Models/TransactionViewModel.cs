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
        }

        [DisplayName("Id")]
        public int IdTransaction { get; set; }

        [DisplayName("Suppliers")]
        public int IdSuppliers { get; set; }
        public IEnumerable<SelectListItem> Suppliers { get; set; }

        public int IdPerson { get; set; }
        public IEnumerable<SelectListItem> Person { get; set; }

        public string PersonName { get; set; }
        public string PersonLastname { get; set; }


        public int DeliveredQuantity { get; set; }
        public DateTime DeliveredDate { get; set; }
        public string Price { get; set; }
        public string Quality { get; set; }
        public bool IsDelete { get; set; }
    }
}
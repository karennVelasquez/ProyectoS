using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGRA2._0.Model;

namespace Front.Models
{
    public class SaleViewModel
    {
        public SaleViewModel()
        {
            Person = new List<SelectListItem>();
            Customer = new List<SelectListItem>();
        }
        [DisplayName("Id")]
        public int IdSale { get; set; }


        [DisplayName("Customer")]
        public int IdCustomer { get; set; }
        public IEnumerable<SelectListItem> Customer { get; set; }
        public int IdPerson { get; set; }
        public IEnumerable<SelectListItem> Person { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }


        public DateTime SaleDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
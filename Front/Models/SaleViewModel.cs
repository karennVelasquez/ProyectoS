using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class SaleViewModel
    {
        [DisplayName("Id")]
        public int IdSale { get; set; }

        [DisplayName("Customer")]
        public int IdCustomer { get; set; }
        public Customer Customer { get; set; }
        public DateTime SaleDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
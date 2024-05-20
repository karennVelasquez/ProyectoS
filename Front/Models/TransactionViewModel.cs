using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class TransactionViewModel
    {
        [DisplayName("Id")]
        public int IdTransaction { get; set; }

        [DisplayName("Suppliers")]
        public int IdSuppliers { get; set; }
        public Suppliers Suppliers { get; set; }

        public int DeliveredQuantity { get; set; }
        public DateTime DeliveredDate { get; set; }
        public string Price { get; set; }
        public string Quality { get; set; }
        public bool IsDelete { get; set; }
    }
}
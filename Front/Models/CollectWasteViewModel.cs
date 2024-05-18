using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class CollectWasteViewModel
    {
        [DisplayName("Id")]
        public int IdCollectWaste { get; set; }
        [DisplayName("CollectWaste")]
        public int IdSuppliers { get; set; }
        public Suppliers Suppliers { get; set; }
        public int IdComposter { get; set; }
        public Composter Composter { get; set; }
        public DateTime CollectionDate { get; set; }
        public int Amount { get; set; }
        public bool IsDelete { get; set; }
    }
}
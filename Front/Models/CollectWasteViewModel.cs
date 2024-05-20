using SGRA2._0.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SGRA2._0.Model;

namespace Front.Models
{
    public class CollectWasteViewModel
    {
        [DisplayName("Id")]
        public int IdCollectWaste { get; set; }

        [DisplayName("Suppliers")]
        public int IdSuppliers { get; set; }
        public Suppliers Suppliers { get; set; }

        [DisplayName("Composter")]
        public int IdComposter { get; set; }
        public Composter Composter { get; set; }
        public DateTime CollectionDate { get; set; }
        public int Amount { get; set; }
        public bool IsDelete { get; set; }
    }
}
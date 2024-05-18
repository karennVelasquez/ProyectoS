using SGRA2._0.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SGRA2._0.Model;

namespace Front.Models
{
    public class SuppliersViewModel
    {
        [DisplayName("Id")]
        public int IdSuppliers { get; set; }
        [DisplayName("Suppliers")]
        public int IdPerson { get; set; }
        public Person Person { get; set; }
        public int IdWasteType { get; set; }
        public WasteType WasteType { get; set; }
        public bool IsDelete { get; set; }
    }
}

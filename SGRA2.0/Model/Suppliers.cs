using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Suppliers
    {
        //Proveedores
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSuppliers { get; set; }
        public  int IdPerson { get; set; }
        public  Person Person { get; set; }
        public  int IdWasteType { get; set; }
        public  WasteType WasteType { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Date { get; set; }
    }
}

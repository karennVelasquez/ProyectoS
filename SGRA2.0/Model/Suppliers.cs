using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Suppliers
    {
        //Proveedores
        [Key]
        public int IdSuppliers { get; set; }
        public required int IdPerson { get; set; }
     //   public required Person Person { get; set; }
        public required int IdWasteType { get; set; }
        // public required WasteType WasteType { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Date { get; set; }
        public string ModifiedBy { get; set; }
    }
}

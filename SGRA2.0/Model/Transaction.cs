using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Transaction
    {
        //Transaccion
        [Key]
        public int IdTransaction { get; set; }
        public required int IdSuppliers { get; set; }
        public  Suppliers Suppliers { get; set; }
        public required int DeliveredQuantity { get; set; }
        public required DateTime DeliveredDate { get; set; }
        public required string Price { get; set; }
        public required string Quality { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Date { get; set; }
        public string ModifiedBy { get; set; }
    }
}

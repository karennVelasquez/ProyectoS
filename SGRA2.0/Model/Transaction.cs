using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Transaction
    {
        //Transaccion
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTransaction { get; set; }
        public  int IdSuppliers { get; set; }
        public  Suppliers Suppliers { get; set; }
        public  int DeliveredQuantity { get; set; }
        public  DateTime DeliveredDate { get; set; }
        public  string Price { get; set; }
        public string Quality { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Date { get; set; }
    }
}

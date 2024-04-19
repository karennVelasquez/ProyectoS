using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Sale
    {
        //Venta
        [Key]
        public int IdSale { get; set; }
        public required int IdCustomer { get; set; }
       // public required Customer Customer { get; set; }
        public required DateTime SaleDate { get; set; }
    }
}

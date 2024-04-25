using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Sale
    {
        //Venta
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSale { get; set; }
        public required int IdCustomer { get; set; }
        public required Customer Customer { get; set; }
        public required DateTime SaleDate { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
        
    }
}

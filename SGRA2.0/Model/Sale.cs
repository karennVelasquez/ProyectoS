using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Sale
    {
        //Venta
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSale { get; set; }
        public int IdCustomer { get; set; }
        public Customer Customer { get; set; }
        public DateTime SaleDate { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
        
    }
}

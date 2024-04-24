using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class CollectWaste
    {
        //RecolectaResiduos
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCollectWaste { get; set; }
        public int IdSuppliers { get; set; }
        public Suppliers Suppliers { get; set; }
        public int IdComposter { get; set; }
        public  Composter Composter { get; set; }
        public  DateTime CollectionDate { get; set; }
        public  int Amount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Date {  get; set; }
    }
}

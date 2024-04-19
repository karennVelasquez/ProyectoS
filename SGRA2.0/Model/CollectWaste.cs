using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class CollectWaste
    {
        //RecolectaResiduos
        [Key]
        public int IdCollectWaste { get; set; }
        public required int IdSuppliers { get; set; }
    //    public required Suppliers Suppliers { get; set; }
        public required int IdComposter { get; set; }
    //    public required Composter Composter { get; set; }
        public required DateTime CollectionDate { get; set; }
        public required int Amount { get; set; }
    }
}

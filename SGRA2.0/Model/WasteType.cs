using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class WasteType
    {
        //TipoResiduos
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdWasteType { get; set; }
        public  string Waste_Type { get; set; }
        public  string Description { get; set; }
        public  string Descomposition { get; set; }
        public bool IsDelete { get; set; }  
        public DateTime Date { get; set; }
    }
}

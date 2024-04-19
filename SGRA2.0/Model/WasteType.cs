using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class WasteType
    {
        //TipoResiduos
        [Key]
        public int IdWasteType { get; set; }
        public required string Waste_Type { get; set; }
        public required string Description { get; set; }
        public required string Descomposition { get; set; }
    }
}

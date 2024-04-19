using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Waste
    {
        //Residuos
        [Key]
        public int IdWaste { get; set; }
        public required int IdWasteType { get; set; }
      //  public required WasteType WasteType { get; set; }
        public required string Humidity { get; set; }
    }
}

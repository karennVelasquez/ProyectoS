using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class FinalCompost
    {
        //FinalCompost
        [Key]
        public int IdFinalCompost { get; set; }
        public required int IdWaste { get; set; }
       public required Waste Waste { get; set; }
        public required string HumidityLevel { get; set; }
        //Nivel de humedad
        public required string FinalPh { get; set; }
        public required string Nutrients { get; set; }
    }
}

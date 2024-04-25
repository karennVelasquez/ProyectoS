using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class FinalCompost
    {
        //FinalCompost
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFinalCompost { get; set; }
        public required int IdWaste { get; set; }
        public required Waste Waste { get; set; }
        public required string HumidityLevel { get; set; }
        //Nivel de humedad
        public required string FinalPh { get; set; }
        public required string Nutrients { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

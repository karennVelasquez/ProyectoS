using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class FinalCompost
    {
        //FinalCompost
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFinalCompost { get; set; }
        public int IdWaste { get; set; }
        public Waste Waste { get; set; }
        public string HumidityLevel { get; set; }
        //Nivel de humedad
        public string FinalPh { get; set; }
        public string Nutrients { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Temperature
    {
        //Temperatura
        [Key]
        public int IdTemperature { get; set; }
        public required int IdWaste { get; set; }
      //  public required Waste Waste { get; set; }
        public required string Decompositiontemperature { get; set; }
        public required string Range { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Date { get; set; }
        public string ModifiedBy { get; set; }
    }
}

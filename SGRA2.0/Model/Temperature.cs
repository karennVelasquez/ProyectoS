using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Temperature
    {
        //Temperatura
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTemperature { get; set; }
        public  int IdWaste { get; set; }
        public  Waste Waste { get; set; }
        public  string Decompositiontemperature { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

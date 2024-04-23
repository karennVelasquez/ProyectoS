using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Waste
    {
        //Residuos
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdWaste { get; set; }
        public int IdWasteType { get; set; }
        public  WasteType WasteType { get; set; }
        public  string Humidity { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

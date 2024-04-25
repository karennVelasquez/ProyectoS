using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Level
    {
        //Nivel
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLevel { get; set; }
        public required int NumLevel { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

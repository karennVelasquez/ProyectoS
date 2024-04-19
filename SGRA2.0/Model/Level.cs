using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Level
    {
        //Nivel
        [Key]
        public int IdLevel { get; set; }
        public required int NumLevel { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Composter
    {
        //Compostador
        [Key]
        public int IdComposter { get; set; }
        public required string Size { get; set; }
        public required string Material { get; set; }
        public required string DrainageSystem { get; set; }
    }
}

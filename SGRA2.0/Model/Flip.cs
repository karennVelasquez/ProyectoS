using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Flip
    {
        //Volteo
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFlip { get; set; }
        public required int IdWaste { get; set; }
        public required Waste Waste  { get; set; }
        public required int Flipfrequency { get; set; }
        public required string UniformedDescription { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

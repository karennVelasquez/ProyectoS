using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Flip
    {
        //Volteo
        [Key]
        public int IdFlip { get; set; }
        public required int IdWaste { get; set; }
        public required Waste Waste  { get; set; }
        public required int Flipfrequency { get; set; }
        public required string UniformedDescription { get; set; }
    }
}

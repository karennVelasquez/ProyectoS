using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Flip
    {
        //Volteo
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFlip { get; set; }
        public int IdWaste { get; set; }
        public Waste Waste  { get; set; }
        public int Flipfrequency { get; set; }
        public string UniformedDescription { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

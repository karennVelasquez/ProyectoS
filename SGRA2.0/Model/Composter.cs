using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Composter
    {
        //Compostador
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComposter { get; set; }
        public  string Size { get; set; }
        public  string Material { get; set; }
        public string DrainageSystem { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Date { get; set; }
    }
}

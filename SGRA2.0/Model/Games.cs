using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Games
    {
        //Partidas
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGames { get; set; }
        public int IdLevel { get; set; }
        public Level Level { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

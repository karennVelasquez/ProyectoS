using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Games
    {
        //Partidas
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGames { get; set; }
        public required int IdUser { get; set; }
        public required User User { get; set; }
        public required int IdLevel { get; set; }
        public required Level Level { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime FinalDate { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

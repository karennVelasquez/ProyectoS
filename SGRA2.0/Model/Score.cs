using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Score
    {
        //Puntaje
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdScore { get; set; }
        public  int IdUser { get; set; }
        public  User User { get; set; }
        public  int IdGames { get; set; }
        public  Games Games { get; set; }
        public  int NumScore { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

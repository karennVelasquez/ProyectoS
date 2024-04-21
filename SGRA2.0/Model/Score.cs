using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Score
    {
        //Puntaje
        [Key]
        public int IdScore { get; set; }
        public required int IdUser { get; set; }
     //   public required User User { get; set; }
        public required int IdGames { get; set; }
     //   public required Games Games { get; set; }
        public required int NumScore { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Date { get; set; }
        public string ModifiedBy { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class AchievementsGames
    {
        //LogrosPartidas
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAchievementsG { get; set; }
        public  int IdGames { get; set; }
        public  Games Games { get; set; }
        public  int IdAchievements { get; set; }
        public  Achievements Achievements { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Date {  get; set; }
    }
}

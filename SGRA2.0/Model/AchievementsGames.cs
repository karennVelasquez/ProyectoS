using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class AchievementsGames
    {
        //LogrosPartidas
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAchievementsG { get; set; }
        public required int IdGames { get; set; }
     //   public required Games Games { get; set; }
        public required int IdAchievements { get; set; }
     //   public required Achievements Achievements { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date {  get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class AchievementsGames
    {
        //LogrosPartidas
        [Key]
        public int IdAchievementsG { get; set; }
        public required int IdGames { get; set; }
     //   public required Games Games { get; set; }
        public required int IdAchievements { get; set; }
     //   public required Achievements Achievements { get; set; }
    }
}

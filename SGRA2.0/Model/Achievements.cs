using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Achievements
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAchievements { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        public int IdGames { get; set; }
        public Games Games { get; set; }
        public bool IsDelete {  get; set; }
        public DateTime? Date {  get; set; }
    }
}

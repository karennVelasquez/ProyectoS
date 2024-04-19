using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Achievements
    {
        //Logros

        [Key]
        public int IdAchievements { get; set; }
        public required string Achievement { get; set; }
    }
}

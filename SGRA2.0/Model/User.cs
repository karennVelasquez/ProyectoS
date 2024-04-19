using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}

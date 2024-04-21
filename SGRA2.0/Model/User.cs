using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public  string Password { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Date { get; set; }

    }
}

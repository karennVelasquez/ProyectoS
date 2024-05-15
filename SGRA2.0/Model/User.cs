using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public  string Password { get; set; }
       //
       // public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }

    }
}

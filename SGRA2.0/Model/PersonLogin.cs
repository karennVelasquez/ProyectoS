using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class PersonLogin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLoginP { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdPerson { get; set; }
        public Person Person { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}

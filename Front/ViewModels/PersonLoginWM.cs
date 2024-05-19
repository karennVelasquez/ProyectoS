using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace Front.ViewModels
{
    public class PersonLoginWM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public int IdPerson { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int DocumentTypes { get; set; }
        public int NumDocument { get; set; }
        public int Document { get; set; }
    }
}

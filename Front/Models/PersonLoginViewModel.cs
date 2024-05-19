using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Front.Models
{
    public class PersonLoginViewModel
    {

        [DisplayName("Id")]
        public int IdLoginP { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IdPerson { get; set; }

        public bool IsDelete { get; set; }

    }
}

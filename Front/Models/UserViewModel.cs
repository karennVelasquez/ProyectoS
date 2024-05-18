using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Front.Models
{
    public class UserViewModel
    {
        [DisplayName("Id")]
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
        //
    }
}

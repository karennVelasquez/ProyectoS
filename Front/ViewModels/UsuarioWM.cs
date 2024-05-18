using System.ComponentModel.DataAnnotations.Schema;
namespace Front.ViewModels
{
    public class UsuarioWM
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

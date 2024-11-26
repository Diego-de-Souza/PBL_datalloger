using System.ComponentModel.DataAnnotations;

namespace AtmoTrack_web_page.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}

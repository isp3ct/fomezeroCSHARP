using System.ComponentModel.DataAnnotations;

namespace fomezero.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email deve estar em um formato válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}

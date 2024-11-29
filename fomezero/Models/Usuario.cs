using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace fomezero.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email deve estar em um formato válido.")]
        public string Email { get; set; } = null!;

        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
        [RegularExpression(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$", ErrorMessage = "O telefone deve estar em um formato válido.")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
        public string Senha { get; set; } = null!;

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        public int TipoUsuarioId { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve ter 14 caracteres (incluindo pontos e traços).")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar em um formato válido (000.000.000-00).")]
        public string? DocumentoIdentificacao { get; set; }

        public virtual ICollection<Doaco> Doacos { get; set; } = new List<Doaco>();
        public virtual ICollection<RetiradaDoaco> RetiradaDoacos { get; set; } = new List<RetiradaDoaco>();
        public virtual TipoUsuario? TipoUsuario { get; set; }
    }
}

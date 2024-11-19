using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Senha { get; set; } = null!;

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        public int TipoUsuarioId { get; set; }

        [StringLength(20, ErrorMessage = "O documento de identificação deve ter no máximo 20 caracteres.")]
        public string? DocumentoIdentificacao { get; set; }

        public virtual ICollection<Doaco> Doacos { get; set; } = new List<Doaco>();

        public virtual ICollection<RetiradaDoaco> RetiradaDoacos { get; set; } = new List<RetiradaDoaco>();

        public virtual TipoUsuario? TipoUsuario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fomezero.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Telefone { get; set; }

        public string Senha { get; set; } = null!;

        public int TipoUsuarioId { get; set; }

        public string? DocumentoIdentificacao { get; set; }

        public virtual ICollection<Doacao> Doacaos { get; set; } = new List<Doacao>();

        public virtual ICollection<RetiradaDoacao> RetiradaDoacaos { get; set; } = new List<RetiradaDoacao>();

        public virtual TipoUsuario? TipoUsuario { get; set; }
    }
}

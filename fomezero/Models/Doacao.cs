using System;
using System.Collections.Generic;

namespace fomezero.Models;

public partial class Doacao
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int? TipoDoacaoId { get; set; }

    public decimal? Valor { get; set; }

    public string? DescricaoAlimento { get; set; }

    public DateTime? DataDoacao { get; set; }

    public virtual ICollection<DoacoesInstituico> DoacoesInstituicos { get; set; } = new List<DoacoesInstituico>();

    public virtual ICollection<RetiradaDoacao> RetiradaDoacaos { get; set; } = new List<RetiradaDoacao>();

    public virtual TipoDoacao? TipoDoacao { get; set; }

    public virtual Usuario? Usuario { get; set; }
}

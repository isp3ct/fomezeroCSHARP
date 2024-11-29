using System;
using System.Collections.Generic;

namespace fomezero.Models;

public partial class TipoDoacao
{
    public int Id { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Doacao> Doacaos { get; set; } = new List<Doacao>();
}

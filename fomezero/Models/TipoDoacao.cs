using System;
using System.Collections.Generic;

namespace fomezero.Models;

public partial class TipoDoacao
{
    public int Id { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Doaco> Doacos { get; set; } = new List<Doaco>();
}

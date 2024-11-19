using System;
using System.Collections.Generic;

namespace fomezero.Models;

public partial class LocaisRetiradum
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Endereco { get; set; } = null!;

    public string HorarioDisponivel { get; set; } = null!;

    public virtual ICollection<RetiradaDoaco> RetiradaDoacos { get; set; } = new List<RetiradaDoaco>();
}

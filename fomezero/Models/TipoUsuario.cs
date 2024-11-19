using System;
using System.Collections.Generic;

namespace fomezero.Models;

public partial class TipoUsuario
{
    public int Id { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

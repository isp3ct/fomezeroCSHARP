using System;
using System.Collections.Generic;

namespace fomezero.Models;

public partial class Instituico
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Contato { get; set; }

    public virtual ICollection<DoacoesInstituico> DoacoesInstituicos { get; set; } = new List<DoacoesInstituico>();
}

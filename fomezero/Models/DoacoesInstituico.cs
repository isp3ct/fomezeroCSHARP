using System;
using System.Collections.Generic;

namespace fomezero.Models;

public partial class DoacoesInstituico
{
    public int Id { get; set; }

    public int? DoacaoId { get; set; }

    public int? InstituicaoId { get; set; }

    public virtual Doacao? Doacao { get; set; }

    public virtual Instituico? Instituicao { get; set; }
}

using System;
using System.Collections.Generic;

namespace fomezero.Models;

public partial class RetiradaDoaco
{
    public int Id { get; set; }

    public int? DoacaoId { get; set; }

    public int? BeneficiarioId { get; set; }

    public int? LocalRetiradaId { get; set; }

    public DateTime DataAgendada { get; set; }

    public DateTime? DataRetirada { get; set; }

    public virtual Usuario? Beneficiario { get; set; }

    public virtual Doaco? Doacao { get; set; }

    public virtual LocaisRetiradum? LocalRetirada { get; set; }
}

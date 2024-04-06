using System;
using System.Collections.Generic;

namespace UFOT.Models;

public partial class Banco
{
    public int BancoId { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Beneficiario> Beneficiarios { get; set; } = new List<Beneficiario>();
}

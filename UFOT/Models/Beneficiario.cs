using System;
using System.Collections.Generic;

namespace UFOT.Models;

public partial class Beneficiario
{
    public int BeneficiarioId { get; set; }

    public string? Nombre { get; set; }

    public string? NumeroCuenta { get; set; }

    public int? BancoId { get; set; }

    public virtual Banco? Banco { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

public partial class Cuenta
{
    [Key]
    [StringLength(20)]
    public string NumeroCuenta { get; set; } = null!;

    [Column("UsuarioID")]
    public int? UsuarioId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Balance { get; set; }

    [StringLength(50)]
    public string? Tipo { get; set; }

    [InverseProperty("CuentaDestino")]
    public virtual ICollection<Movimiento> MovimientoCuentaDestinos { get; set; } = new List<Movimiento>();

    [InverseProperty("CuentaOrigen")]
    public virtual ICollection<Movimiento> MovimientoCuentaOrigens { get; set; } = new List<Movimiento>();

    [InverseProperty("NumeroCuentaNavigation")]
    public virtual ICollection<PagosPrestamo> PagosPrestamos { get; set; } = new List<PagosPrestamo>();

    [InverseProperty("CuentaOrigenNavigation")]
    public virtual ICollection<TransaccionBeneficiario> TransaccionBeneficiarios { get; set; } = new List<TransaccionBeneficiario>();

    [InverseProperty("CuentaOrigenNavigation")]
    public virtual ICollection<TransaccionCuentum> TransaccionCuentumCuentaOrigenNavigations { get; set; } = new List<TransaccionCuentum>();

    [InverseProperty("NumeroCuentaNavigation")]
    public virtual ICollection<TransaccionCuentum> TransaccionCuentumNumeroCuentaNavigations { get; set; } = new List<TransaccionCuentum>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("Cuenta")]
    public virtual Usuario? Usuario { get; set; }
}

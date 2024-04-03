using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

public partial class TransaccionCuentum
{
    [Key]
    [Column("TransaccionID")]
    public int TransaccionId { get; set; }

    [StringLength(20)]
    public string? CuentaOrigen { get; set; }

    [StringLength(20)]
    public string? NumeroCuenta { get; set; }

    [StringLength(255)]
    public string? Descripcion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    [StringLength(50)]
    public string? Tipo { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Monto { get; set; }

    [ForeignKey("CuentaOrigen")]
    [InverseProperty("TransaccionCuentumCuentaOrigenNavigations")]
    public virtual Cuenta? CuentaOrigenNavigation { get; set; }

    [ForeignKey("NumeroCuenta")]
    [InverseProperty("TransaccionCuentumNumeroCuentaNavigations")]
    public virtual Cuenta? NumeroCuentaNavigation { get; set; }

    [ForeignKey("TransaccionId")]
    [InverseProperty("TransaccionCuentum")]
    public virtual Transaccione Transaccion { get; set; } = null!;
}

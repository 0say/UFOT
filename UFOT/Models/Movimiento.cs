using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

public partial class Movimiento
{
    [Key]
    [Column("MovimientoID")]
    public int MovimientoId { get; set; }

    [Column("PrestamoID")]
    public int? PrestamoId { get; set; }

    [Column("CuentaOrigenID")]
    [StringLength(20)]
    public string? CuentaOrigenId { get; set; }

    [Column("CuentaDestinoID")]
    [StringLength(20)]
    public string? CuentaDestinoId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Monto { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    [ForeignKey("CuentaDestinoId")]
    [InverseProperty("MovimientoCuentaDestinos")]
    public virtual Cuenta? CuentaDestino { get; set; }

    [ForeignKey("CuentaOrigenId")]
    [InverseProperty("MovimientoCuentaOrigens")]
    public virtual Cuenta? CuentaOrigen { get; set; }

    [ForeignKey("PrestamoId")]
    [InverseProperty("Movimientos")]
    public virtual Prestamo? Prestamo { get; set; }
}

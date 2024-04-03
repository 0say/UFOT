using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

[Table("PagosPrestamo")]
public partial class PagosPrestamo
{
    [Key]
    [Column("PagoID")]
    public int PagoId { get; set; }

    [Column("PrestamoID")]
    public int? PrestamoId { get; set; }

    [StringLength(20)]
    public string? NumeroCuenta { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MontoPagado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaPago { get; set; }

    [ForeignKey("NumeroCuenta")]
    [InverseProperty("PagosPrestamos")]
    public virtual Cuenta? NumeroCuentaNavigation { get; set; }

    [ForeignKey("PrestamoId")]
    [InverseProperty("PagosPrestamos")]
    public virtual Prestamo? Prestamo { get; set; }
}

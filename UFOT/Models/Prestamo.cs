using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

public partial class Prestamo
{
    [Key]
    [Column("PrestamoID")]
    public int PrestamoId { get; set; }

    [Column("UsuarioID")]
    public int? UsuarioId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Monto { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal? TasaInteres { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaInicio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaFin { get; set; }

    [InverseProperty("Prestamo")]
    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    [InverseProperty("Prestamo")]
    public virtual ICollection<PagosPrestamo> PagosPrestamos { get; set; } = new List<PagosPrestamo>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("Prestamos")]
    public virtual Usuario? Usuario { get; set; }
}

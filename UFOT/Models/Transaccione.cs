using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

public partial class Transaccione
{
    [Key]
    [Column("TransaccionID")]
    public int TransaccionId { get; set; }

    [Column("UsuarioID")]
    public int? UsuarioId { get; set; }

    [StringLength(255)]
    public string? Descripcion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    [StringLength(50)]
    public string? Tipo { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Monto { get; set; }

    [InverseProperty("Transaccion")]
    public virtual TransaccionBeneficiario? TransaccionBeneficiario { get; set; }

    [InverseProperty("Transaccion")]
    public virtual TransaccionCuentum? TransaccionCuentum { get; set; }

    [ForeignKey("UsuarioId")]
    [InverseProperty("Transacciones")]
    public virtual Usuario? Usuario { get; set; }
}

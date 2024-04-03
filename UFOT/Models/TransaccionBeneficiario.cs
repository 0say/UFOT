using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

[Table("TransaccionBeneficiario")]
public partial class TransaccionBeneficiario
{
    [Key]
    [Column("TransaccionID")]
    public int TransaccionId { get; set; }

    [Column("BeneficiarioID")]
    public int? BeneficiarioId { get; set; }

    [StringLength(255)]
    public string? Descripcion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    [StringLength(50)]
    public string? Tipo { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Monto { get; set; }

    [StringLength(20)]
    public string? CuentaOrigen { get; set; }

    [ForeignKey("BeneficiarioId")]
    [InverseProperty("TransaccionBeneficiarios")]
    public virtual Beneficiario? Beneficiario { get; set; }

    [ForeignKey("CuentaOrigen")]
    [InverseProperty("TransaccionBeneficiarios")]
    public virtual Cuenta? CuentaOrigenNavigation { get; set; }

    [ForeignKey("TransaccionId")]
    [InverseProperty("TransaccionBeneficiario")]
    public virtual Transaccione Transaccion { get; set; } = null!;
}

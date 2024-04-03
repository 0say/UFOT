using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

public partial class Beneficiario
{
    [Key]
    [Column("BeneficiarioID")]
    public int BeneficiarioId { get; set; }

    [StringLength(100)]
    public string? Nombre { get; set; }

    [StringLength(20)]
    public string? NumeroCuenta { get; set; }

    [Column("BancoID")]
    public int? BancoId { get; set; }

    [ForeignKey("BancoId")]
    [InverseProperty("Beneficiarios")]
    public virtual Banco? Banco { get; set; }

    [InverseProperty("Beneficiario")]
    public virtual ICollection<TransaccionBeneficiario> TransaccionBeneficiarios { get; set; } = new List<TransaccionBeneficiario>();
}

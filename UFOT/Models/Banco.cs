using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

public partial class Banco
{
    [Key]
    [Column("BancoID")]
    public int BancoId { get; set; }

    [StringLength(100)]
    public string? Nombre { get; set; }

    [InverseProperty("Banco")]
    public virtual ICollection<Beneficiario> Beneficiarios { get; set; } = new List<Beneficiario>();
}

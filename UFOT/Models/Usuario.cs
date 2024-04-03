using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

[Index("Documento", Name = "UQ__Usuarios__AF73706D41F6F285", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("UsuarioID")]
    public int UsuarioId { get; set; }

    [StringLength(16)]
    public string? Documento { get; set; }

    [Column("Usuario")]
    [StringLength(16)]
    [DisplayName("Usuario")]
    public string? Usuario1 { get; set; }

    [StringLength(100)]
    public string? Nombre { get; set; }

    [StringLength(100)]
    public string? Apellido { get; set; }

    [StringLength(32)]
    public string? Clave { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    public string? Direccion { get; set; }

    [StringLength(10)]
    public string? Telefono { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    [InverseProperty("Usuario")]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    [InverseProperty("Usuario")]
    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}

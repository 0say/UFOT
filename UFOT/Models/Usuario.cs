using System;
using System.Collections.Generic;

namespace UFOT.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? Documento { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Clave { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string Rol { get; set; } = null!;

    public virtual ICollection<Beneficiario> Beneficiarios { get; set; } = new List<Beneficiario>();
}

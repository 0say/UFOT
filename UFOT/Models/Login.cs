using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UFOT.Models;

public partial class Login
    
{
    [DisplayName("Usuario")]
    public string? NombreUsuario { get; set; }
    public string? Clave { get; set; }
}

  

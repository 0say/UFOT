using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UFOT.Models;

public partial class Log
{
    public string? Mensaje { get; set; }

    public string? TipoMensaje { get; set; }

    public DateTime? Fecha { get; set; }
    [Key] 
    public int LogId { get; set; }
}

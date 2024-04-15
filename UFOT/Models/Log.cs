using System;
using System.Collections.Generic;

namespace UFOT.Models;

public partial class Log
{
    public string? Mensaje { get; set; }

    public string? TipoMensaje { get; set; }

    public DateTime? Fecha { get; set; }

    public int LogId { get; set; }
}

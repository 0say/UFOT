using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using UFOT.Controllers;
using UFOT.Data;
using UFOT.Models;

namespace UFOT
{
    public class LogService : Controller
    {
        public readonly BancoWebContext _context;
        public readonly INotyfService _notyf;

        public LogService(BancoWebContext context, INotyfService notyf)
        {      
            _context = context;
            _notyf = notyf;
        }

        public void AgregarLog(string mensaje, string tipoMensaje)
        {
            Log log = new Log();
            log.Mensaje = mensaje;
            log.TipoMensaje = tipoMensaje;
            log.Fecha = DateTime.Now;
            
            _context.Logs.Add(log);
            _notyf.Information(mensaje);
            _context.SaveChanges();
                    

        }
    }
}

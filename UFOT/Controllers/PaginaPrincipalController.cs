using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using UFOT.Data;
using UFOT.Models;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Net.WebSockets;

namespace UFOT.Controllers
{
    public class PaginaPrincipalController : BaseController
    {
       

        public PaginaPrincipalController(LogService logger, BancoWebContext context, INotyfService notyf) : base(logger, context, notyf)
        {
        
        }


        public IActionResult Index(Login login)
        {
            // Verificar si el usuario ya ha iniciado sesión
            var userId = HttpContext.Session.GetString("UserID");
            if (!string.IsNullOrEmpty(userId))
            {
                // Si el usuario ya ha iniciado sesión, simplemente mostrar la vista
                return View();
            }

            // Si el usuario no ha iniciado sesión previamente, proceder con la autenticación
            Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.NombreUsuario == login.NombreUsuario && x.Clave == login.Clave);
            if (usuario == null)
            {
                _notyf.Error("Usuario o contraseña incorrectos");
                return RedirectToAction("Index", "Home");
            }

            // Autenticación exitosa, establecer la sesión
            HttpContext.Session.SetString("UserID", usuario.UsuarioId.ToString());

            if (usuario.Rol == "Admin")
            {
                _logger.AgregarLog("Se ha iniciado sesión como admin", "Información");
                return RedirectToAction("Index", "ADM");
            }

            return View();
        }




    }
}

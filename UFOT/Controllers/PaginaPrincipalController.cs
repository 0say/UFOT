using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UFOT.Data;
using UFOT.Models;

namespace UFOT.Controllers
{
    public class PaginaPrincipalController : BaseController
    {
        public PaginaPrincipalController(LogService logger, BancoWebContext context, INotyfService notyf) : base(logger, context, notyf)
        {
        }

        public IActionResult Index(Login login)
        {
            HttpContext.Session.Clear();
            // Verificar si el usuario ya ha iniciado sesión
            var userId = HttpContext.Session.GetString("UserID");
            var userRl = HttpContext.Session.GetString("UserRL");
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userRl))
            {
                if (userRl == "admin")
                {
                    _logger.AgregarLog("Se ha iniciado sesión como admin", "Información");
                    return RedirectToAction("Index", "ADM");
                }
                return View();
            }

            // Si el usuario no ha iniciado sesión previamente, proceder con la autenticación
            Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.NombreUsuario == login.NombreUsuario);
            if (usuario == null)
            {
                _notyf.Error("Usuario o contraseña incorrectos");
                return RedirectToAction("Index", "Home");
            }

            // Verificar la contraseña
            if (usuario.Clave != login.Clave)
            {
                _notyf.Error("Usuario o contraseña incorrectos");
                return RedirectToAction("Index", "Home");
            }

            // Autenticación exitosa, establecer la sesión
            HttpContext.Session.SetString("UserID", usuario.UsuarioId.ToString());
            HttpContext.Session.SetString("UserRL", usuario.Rol);

            if (usuario.Rol == "Admin")
            {
                _logger.AgregarLog("Se ha iniciado sesión como admin", "Información");
                return RedirectToAction("Index", "ADM");
            }

            return View();
        }

    }
}

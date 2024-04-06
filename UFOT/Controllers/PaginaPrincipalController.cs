using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using UFOT.Data;
using UFOT.Models;
using System.Collections.Generic;

namespace UFOT.Controllers
{
    public class PaginaPrincipalController : BaseController
    {
       

        public PaginaPrincipalController(LogService logger, BancoWebContext context, INotyfService notyf) : base(logger, context, notyf)
        {
        
        }


        public IActionResult Index(Login login)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.NombreUsuario == login.NombreUsuario && x.Clave == login.Clave);
            List<Usuario> usuario1 = _context.Usuarios.ToList();
            if (usuario == null)
            {
                _notyf.Error("Usuario o contraseña incorrectos");
                return RedirectToAction("Index", "Home");
              
            }
            if (usuario.Rol == "Admin") 
            {
                _logger.AgregarLog("Se ha iniciado sesión como admin", "Información");
                return RedirectToAction("Index", "ADM");
            }
            //  List<Cuenta> cuentas = _context.Cuentas.ToList(); 

            //   List<Cuenta> cuentas1 = _context.Cuentas.Where(x => x.UsuarioId == usuario.UsuarioId).ToList();

            return View();
        }

    }
}

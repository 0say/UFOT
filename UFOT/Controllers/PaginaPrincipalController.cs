using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using UFOT.Data;
using UFOT.Models;
using System.Collections.Generic;

namespace UFOT.Controllers
{
    public class PaginaPrincipalController : Controller
    {
        private readonly BancoWebContext _context;
        private readonly INotyfService _notyf;

        public PaginaPrincipalController(BancoWebContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }
      

        public void Index(Usuario login)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.Usuario1 == login.Usuario1 && x.Clave == login.Clave);

            if (usuario == null)
            {
                _notyf.Error("Usuario o Contraseña incorrectos.");
                RedirectToAction("Index", "Home");
            }

            List<Cuenta> cuentas = _context.Cuentas.ToList();      
             View(cuentas);
        }

    }
}

using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Diagnostics;
using UFOT.Data;
using UFOT.Models;

namespace UFOT.Controllers
{
    public class HomeController : Controller
    {
        private readonly BancoWebContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;



        public HomeController(ILogger<HomeController> logger,BancoWebContext context, INotyfService notyf )
        {
            _logger = logger;
            _context = context; 
            _notyf = notyf;
        }

        public IActionResult Index()
        {
           
            return View();
        }
    

        public IActionResult Privacy()
        {
            return View();
        }

        ////public IActionResult Inicio(Usuario login)
        ////{  
        ////    Usuario usuarios = _context.Usuarios.FirstOrDefault(x => x.Usuario1 == login.Usuario1 && x.Clave == login.Clave);

        ////    if (usuarios == null)
        ////        return RedirectToAction("Index");

        ////    List<Cuenta> cuentas = _context.Cuentas.ToList();
        ////    return View(cuentas);

        ////}

        public IActionResult Beneficiario()
        { 
            return View(); 
        }

        public IActionResult Prestamo() 
        { 
            return View(); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult TransferirBeneficiario()
        {
            return View();
        }
        public IActionResult transferirTerceros()
        {
            return View();
        }

        public IActionResult transferirEntreCuentas()
        {
            return View();
        }
    }
}

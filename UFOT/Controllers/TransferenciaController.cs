using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using UFOT.Data;

namespace UFOT.Controllers
{
    public class TransferenciaController : Controller
    {
        private readonly BancoWebContext _context;
        private readonly INotyfService _notyf;
        public TransferenciaController(BancoWebContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        public IActionResult TransferirBeneficiario()
        {
            return View();
            
        }

        public IActionResult TransferirEntreCuentas() { return View(); }


        public IActionResult TransferirEntreTerceros() { return View(); }   



    }
}

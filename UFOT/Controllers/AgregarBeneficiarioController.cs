using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UFOT.Data;

namespace UFOT.Controllers
{
    public class AgregarBeneficiarioController : BaseController
    {
        public AgregarBeneficiarioController(LogService logger, BancoWebContext context, INotyfService notyf) : base(logger, context, notyf)
        {
        }

        public IActionResult Index()
        {
            ViewBag.Bancos = new SelectList(_context.Bancos, "BancoId", "Nombre");
            return View();
           
        }
    }
}

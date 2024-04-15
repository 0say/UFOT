using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UFOT.Data;
using UFOT.Models;

namespace UFOT.Controllers
{

    public class AgregarBeneficiarioController : BaseController
    {
        public AgregarBeneficiarioController(LogService logger, BancoWebContext context, INotyfService notyf, HttpClient httpClient) : base(logger, context, notyf, httpClient)
        {
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Bancos = new SelectList(_context.Bancos, "BancoId", "Nombre");
            Beneficiario Beneficiario = new Beneficiario();
            return View(Beneficiario);

        }
        
        [HttpPost]

        public IActionResult Index(Beneficiario Beneficiario)
        {
            
            try
            {
                if (Beneficiario.Nombre == null)
                {
                    _notyf.Error("No puede estar vacio");
                    return View(Beneficiario);
                }

                if (Beneficiario.NumeroCuenta == null)
                {
                    _notyf.Error("No puede estar vacio");
                    return View(Beneficiario);
                }

                if (Beneficiario.Banco == null)
                {
                    _notyf.Error("No puede estar vacio");
                    return View(Beneficiario);
                }

                else
                {
                    // Agregar el nuevo usuario al contexto
                    var userId = HttpContext.Session.GetString("UserID");
                    Beneficiario.UsuarioId = Int32.Parse(userId);
                    _context.Beneficiarios.Add(Beneficiario);
                    // Guardar los cambios en la base de datos
                    _context.SaveChanges();
                    // Redirigir al usuario a la acción Index después de agregar el usuario
                    return RedirectToAction("Index", "Pagina Principal");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra al guardar el usuario en la base de datos
                // Por ejemplo, puedes agregar el manejo de errores adecuado, como mostrar un mensaje de error al usuario o registrar la excepción
                _notyf.Error("Error Al crear Beneficiario");
                // Volver a la vista de creación con los datos ingresados por el usuario
                return View(Beneficiario);
            }

            // Si el modelo no es válido, volver a la vista de creación con los datos ingresados por el usuario
        }
    }
}

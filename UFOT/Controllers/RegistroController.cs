using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UFOT.Data;
using UFOT.Models;

namespace UFOT.Controllers
{
    
    public class RegistroController : Controller
    {
        private readonly BancoWebContext _context;
        public RegistroController(BancoWebContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            Usuario usuario = new Usuario();
            return View(usuario);

        }
        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                

                try
                {
                    if (usuario.Clave.Length < 8)
                    {
                        ModelState.AddModelError(string.Empty, "Intente con una contraseña mas larga");
                        return View(usuario);
                    }
                    else
                    {
                        // Agregar el nuevo usuario al contexto
                        _context.Usuarios.Add(usuario);
                        // Guardar los cambios en la base de datos
                        _context.SaveChanges();
                        // Redirigir al usuario a la acción Index después de agregar el usuario
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que ocurra al guardar el usuario en la base de datos
                    // Por ejemplo, puedes agregar el manejo de errores adecuado, como mostrar un mensaje de error al usuario o registrar la excepción
                    ModelState.AddModelError(string.Empty, "Error Al crear el usuario, intente denuevo");
                    // Volver a la vista de creación con los datos ingresados por el usuario
                    return View(usuario);
                }
            }
            // Si el modelo no es válido, volver a la vista de creación con los datos ingresados por el usuario
            return View(usuario);
        }
    }
}

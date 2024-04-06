using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UFOT.Data;
using UFOT.Models;

namespace UFOT.Controllers
{
    
    public class RegistroController : BaseController
    {
        public RegistroController(LogService logger, BancoWebContext context, INotyfService notyf) : base(logger, context, notyf)
        {
        }

        //private readonly BancoWebContext _context;
        //public RegistroController(BancoWebContext context)
        //{
        //    _context = context;
        //}
        [HttpGet]
        public IActionResult Index()
        {
            Usuario usuario = new Usuario();
            return View(usuario);

        }
        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            usuario.Rol = "Cliente";     
                try
                {
                    if (usuario.Clave.Length < 8)
                    {
                        ModelState.AddModelError(string.Empty, "Intente con una contraseña mas larga");
                        return View(usuario);
                    }

                if (usuario.NombreUsuario.Length <= 4)
                {
                    ModelState.AddModelError(string.Empty, "Intente con una usuario mas largo");
                    return View(usuario);
                }

                if (usuario.Telefono.Length < 10)
                {
                    ModelState.AddModelError(string.Empty, "Intente con un numero de telefono existente");
                    return View(usuario);
                }
                else
                    {
                        // Agregar el nuevo usuario al contexto
                        _context.Usuarios.Add(usuario);
                        // Guardar los cambios en la base de datos
                        _context.SaveChanges();
                        // Redirigir al usuario a la acción Index después de agregar el usuario
                        return RedirectToAction("Home", "Index");
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
            
            // Si el modelo no es válido, volver a la vista de creación con los datos ingresados por el usuario
        }
    }
}

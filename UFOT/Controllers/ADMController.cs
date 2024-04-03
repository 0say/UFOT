using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UFOT.Data;
using UFOT.Models;

namespace UFOT.Controllers
{
    public class ADMController : Controller
    {
        private readonly BancoWebContext _context;

        public ADMController(BancoWebContext context)
        {
          _context = context;
        }
        public IActionResult Index()
        {
            List<Usuario> usuarios = _context.Usuarios.ToList();

            return View(usuarios);

         
        }
        [HttpGet]
        public IActionResult Create()
        {
            Usuario usuario = new Usuario();
            return View(usuario);
        }
        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {

            if (ModelState.IsValid)
            { 
                try
                {
                    // Agregar el nuevo usuario al contexto
                    _context.Usuarios.Add(usuario);
                    // Guardar los cambios en la base de datos
                    _context.SaveChanges();
                    // Redirigir al usuario a la acción Index después de agregar el usuario
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que ocurra al guardar el usuario en la base de datos
                    // Por ejemplo, puedes agregar el manejo de errores adecuado, como mostrar un mensaje de error al usuario o registrar la excepción
                    ModelState.AddModelError(string.Empty, "Error al guardar el usuario en la base de datos.");
                    // Volver a la vista de creación con los datos ingresados por el usuario
                    return View(usuario);
                }
            }
            // Si el modelo no es válido, volver a la vista de creación con los datos ingresados por el usuario
            return View(usuario);
        }




        public IActionResult Details(int id)
        {
            // Buscar el usuario con el UsuarioId proporcionado en la base de datos
            Usuario usuario = _context.Usuarios.FirstOrDefault(p => p.UsuarioId == id);

            // Verificar si se encontró el usuario
            if (usuario == null)
            {
                // Si no se encuentra el usuario, redirigir a una página de error o hacer algo apropiado
                return NotFound(); // Por ejemplo, devuelve un error 404
            }

            // Pasar el usuario encontrado a la vista
            return View(usuario);
        }
        public IActionResult Edit(int id)
        {
            // Buscar el usuario con el UsuarioId proporcionado en la base de datos
            Usuario usuario = _context.Usuarios.FirstOrDefault(p => p.UsuarioId == id);

            // Verificar si se encontró el usuario
            if (usuario == null)
            {
                // Si no se encuentra el usuario, redirigir a una página de error o hacer algo apropiado
                return NotFound(); // Por ejemplo, devuelve un error 404
            }

            // Pasar el usuario encontrado a la vista de edición
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el usuario en el contexto y guardar los cambios en la base de datos
                    _context.Update(usuario);
                    _context.SaveChanges();

                    // Redirigir al usuario a la acción Index después de editar el usuario
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que ocurra al guardar el usuario en la base de datos
                    // Aquí puedes agregar el manejo de errores adecuado, como mostrar un mensaje de error al usuario o registrar la excepción
                    ModelState.AddModelError(string.Empty, "Error al guardar los cambios del usuario en la base de datos.");
                    // Regresar a la vista de edición con los datos ingresados por el usuario
                    return View(usuario);
                }
            }
            // Si el modelo no es válido, regresar a la vista de edición con los datos ingresados por el usuario
            return View(usuario);
        }

        public IActionResult Delete(int id)
        {
            // Buscar el usuario con el UsuarioId proporcionado en la base de datos
            Usuario usuario = _context.Usuarios.FirstOrDefault(p => p.UsuarioId == id);

            // Verificar si se encontró el usuario
            if (usuario == null)
            {
                // Si no se encuentra el usuario, redirigir a una página de error o hacer algo apropiado
                return NotFound(); // Por ejemplo, devuelve un error 404
            }

            // Pasar el usuario encontrado a la vista de confirmación de eliminación
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Buscar el usuario con el UsuarioId proporcionado en la base de datos
            Usuario usuario = _context.Usuarios.Find(id);

            // Verificar si se encontró el usuario
            if (usuario == null)
            {
                // Si no se encuentra el usuario, redirigir a una página de error o hacer algo apropiado
                return NotFound(); // Por ejemplo, devuelve un error 404
            }

            // Eliminar el usuario del contexto y guardar los cambios en la base de datos
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            // Redirigir al usuario a la acción Index después de eliminar el usuario
            return RedirectToAction(nameof(Index));
        }





    }
}

using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UFOT;
using System.Data;
using System.Linq;
using System.Net.Http;
using UFOT.Data;
using UFOT.Models;
using Newtonsoft.Json;

namespace UFOT.Controllers
{
    public class PaginaPrincipalController : BaseController
    {
        public PaginaPrincipalController(LogService logger, BancoWebContext context, INotyfService notyf, HttpClient httpClient) : base(logger, context, notyf, httpClient)
        {
        }

        string _baseURL = "https://bankintegrationlayer20240414102922.azurewebsites.net/";

        public async Task<IActionResult> Index(Login login)
        {
            HttpContext.Session.Clear();
            var userId = HttpContext.Session.GetString("UserID");
            var userRl = HttpContext.Session.GetString("UserRL");

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userRl))
            {
                if (userRl == "admin")
                {
                    _logger.AgregarLog("Se ha iniciado sesión como admin", "Información");
                    return RedirectToAction("Index", "ADM");
                }

                // Hacer una solicitud HTTP GET a la API para obtener todas las cuentas
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseURL}/api/cuentas/123456789");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Cuenta> cuentas = JsonConvert.DeserializeObject<List<Cuenta>>(json);
                    // Utilizar las cuentas obtenidas
                    return View(cuentas);
                }
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

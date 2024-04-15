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
using System.Text;

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

            var userId = HttpContext.Session.GetString("UserID");
            var userRl = HttpContext.Session.GetString("UserRL");

            //Redireccion Inicio
            // Verificar si el usuario ya inició sesión
            // Verificar si el usuario ya inició sesión
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userRl))
            {
                if (userRl == "admin")
                {
                    _logger.AgregarLog("Se ha iniciado sesión como admin", "Información");
                    return RedirectToAction("Index", "ADM");
                }

                // Si la sesión ya está iniciada y no es admin, realizar la solicitud HTTP
                var userDc = HttpContext.Session.GetString("UserDC");
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseURL}/{userDc}");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Cuenta> cuentas = JsonConvert.DeserializeObject<List<Cuenta>>(json);
                    // Utilizar las cuentas obtenidas
                    return View(cuentas);
                }
                else
                {
                    _notyf.Error("No se pudieron obtener las cuentas del usuario");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // Si no hay sesión iniciada, continuar con la autenticación
                Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.NombreUsuario == login.NombreUsuario);

                if (usuario == null || usuario.Clave != login.Clave)
                {
                    _notyf.Error("Usuario o contraseña incorrectos");
                    return RedirectToAction("Index", "Home");
                }

                // Autenticación exitosa, establecer la sesión  
                HttpContext.Session.SetString("UserID", usuario.UsuarioId.ToString());
                HttpContext.Session.SetString("UserRL", usuario.Rol);
                HttpContext.Session.SetString("UserDC", usuario.Documento.ToString());

                // Si es admin, redirigir a la página de administrador
                if (usuario.Rol == "Admin")
                {
                    _logger.AgregarLog("Se ha iniciado sesión como admin", "Información");
                    return RedirectToAction("Index", "ADM");
                }


               
                // Realizar la solicitud HTTP para obtener las cuentas del usuario
                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseURL}/{usuario.Documento}");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Cuenta> cuentas = JsonConvert.DeserializeObject<List<Cuenta>>(json);
                    // Utilizar las cuentas obtenidas
                    return View(cuentas);
                }
                else
                {
                    _notyf.Error("No se pudieron obtener las cuentas del usuario");
                    return RedirectToAction("Index", "Home");
                }
 
            }

        }


        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }

}

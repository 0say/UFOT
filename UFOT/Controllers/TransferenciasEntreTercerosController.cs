using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UFOT.Data;
using UFOT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UFOT.Controllers
{
    public class TransferenciasEntreTercerosController : BaseController
    {
        public TransferenciasEntreTercerosController(LogService logger, BancoWebContext context, INotyfService notyf, HttpClient httpClient) : base(logger, context, notyf, httpClient)
        {
        }

        string _baseURL = "https://bankintegrationlayer20240414102922.azurewebsites.net/";

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserID");
                var userRl = HttpContext.Session.GetString("UserRL");

                // Verificar si el usuario ya inició sesión
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userRl))
                {
                    var userDc = HttpContext.Session.GetString("UserDC");
                    HttpResponseMessage response = await _httpClient.GetAsync($"{_baseURL}/{userDc}");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<Cuenta> cuentas = JsonConvert.DeserializeObject<List<Cuenta>>(json);

                        // Agregar las cuentas a ViewBag.Cuentas
                        ViewBag.Cuentas = cuentas;

                        // Utilizar las cuentas obtenidas
                        return View();
                    }
                    else
                    {
                        _notyf.Error("No se pudieron obtener las cuentas del usuario");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    // Si el usuario no ha iniciado sesión, redirigir a la página de inicio
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _notyf.Error($"Error al obtener las cuentas del usuario: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(int cuentaOrigen, int cuentaDestino, double monto)
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserID");
                var userRl = HttpContext.Session.GetString("UserRL");

                // Verificar si el usuario ya inició sesión
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userRl))
                {
                    var userDc = HttpContext.Session.GetString("UserDC");

                    // Construir el objeto de transferencia
                    var transferencia = new
                    {
                        idCuentaOrigen = cuentaOrigen,
                        idCuentaDestino = cuentaDestino,
                        idTipoTransaccion = 7,
                        monto = monto,
                        idTipoMovimiento = 1
                    };

                    // Convertir el objeto de transferencia a JSON
                    var jsonTransferencia = JsonConvert.SerializeObject(transferencia);

                    // Enviar la solicitud HTTP POST para realizar la transferencia
                    HttpResponseMessage response = await _httpClient.PostAsync($"{_baseURL}/api/Client/Transferencia", new StringContent(jsonTransferencia, System.Text.Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        _notyf.Success("Transferencia realizada exitosamente");
                        return RedirectToAction("Index", "PaginaPrincipal");
                    }
                    else
                    {
                        _notyf.Error("No se pudo completar la transferencia");
                        return RedirectToAction("Index", "PaginaPrincipal");
                    }
                }
                else
                {
                    // Si el usuario no ha iniciado sesión, redirigir a la página de inicio
                    return RedirectToAction("Index", "PaginaPrincipal");
                }
            }
            catch (Exception ex)
            {
                _notyf.Error($"Error al realizar la transferencia: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
        }


    }
}

using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UFOT.Data;
using UFOT.Models;

namespace UFOT.Controllers
{
    public class TransferenciasBeneficiariosController : BaseController
    {
        public TransferenciasBeneficiariosController(LogService logger, BancoWebContext context, INotyfService notyf, HttpClient httpClient) : base(logger, context, notyf, httpClient)
        {
        }

        string _baseURL = "https://bankintegrationlayer20240414102922.azurewebsites.net/";

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserID");

                // Verificar si el usuario ya inició sesión
                if (!string.IsNullOrEmpty(userId))
                {
                    var userDc = HttpContext.Session.GetString("UserDC");

                    // Obtener cuentas del usuario
                    HttpResponseMessage response = await _httpClient.GetAsync($"{_baseURL}/{userDc}");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<Cuenta> cuentas = JsonConvert.DeserializeObject<List<Cuenta>>(json);

                        // Obtener los beneficiarios asociados al UsuarioID
                        List<Beneficiario> listaBeneficiarios = await _context.Beneficiarios
                            .Where(b => b.UsuarioId == int.Parse(userId))
                            .ToListAsync();

                        // Pasar las cuentas y beneficiarios a la vista
                        ViewBag.Cuentas = cuentas;
                        ViewBag.Beneficiarios = listaBeneficiarios;

                        return View();
                    }
                    else
                    {
                        _notyf.Error("No se pudieron obtener las cuentas del usuario");
                        return RedirectToAction("Index", "PaginaPrincipal");
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
                _notyf.Error($"Error al obtener los beneficiarios del usuario: {ex.Message}");
                _logger.AgregarLog($"{ex.Message}", "Error");
                return RedirectToAction("Index", "PaginaPrincipal");
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> Index(int cuentaOrigen, string beneficiario, double monto)
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserID");
                var userRl = HttpContext.Session.GetString("UserRL");

                // Verificar si el usuario ya inició sesión
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userRl))
                {
                    var userDc = HttpContext.Session.GetString("UserDC");

                    // Obtener el ID del beneficiario seleccionado basado en el número de cuenta
                    var beneficiarioSeleccionado = await _context.Beneficiarios.FirstOrDefaultAsync(b => b.NumeroCuenta == beneficiario);

                    // Declaring transferencia here
                    dynamic transferencia = null;

                    if (beneficiarioSeleccionado != null)
                    {
                        if (beneficiarioSeleccionado.BancoId != 1)
                        {
                            transferencia = new
                            {
                                idCuentaOrigen = cuentaOrigen,
                                idCuentaDestino = beneficiarioSeleccionado.NumeroCuenta, // Usar el ID del beneficiario como cuenta destino
                                idTipoTransaccion = 7,
                                monto = monto,
                                idTipoMovimiento = 2
                            };
                        }
                        else
                        {
                            // Construir el objeto de transferencia
                            transferencia = new
                            {
                                idCuentaOrigen = cuentaOrigen,
                                idCuentaDestino = beneficiarioSeleccionado.NumeroCuenta, // Usar el ID del beneficiario como cuenta destino
                                idTipoTransaccion = 7,
                                monto = monto,
                                idTipoMovimiento = 1
                            };
                        }

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
                        _notyf.Error("Beneficiario no encontrado");
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

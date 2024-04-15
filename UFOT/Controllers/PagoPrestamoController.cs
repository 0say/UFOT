using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UFOT.Data;
using UFOT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using QuickType;

namespace UFOT.Controllers
{
    public class PagoPrestamoController : BaseController
    {
        public PagoPrestamoController(LogService logger, BancoWebContext context, INotyfService notyf, HttpClient httpClient) : base(logger, context, notyf, httpClient)
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
                    HttpResponseMessage prestamosResponse = await _httpClient.GetAsync($"{_baseURL}/prestamos/{userId}");

                    if (prestamosResponse.IsSuccessStatusCode)
                    {
                        string jsonPrestamos = await prestamosResponse.Content.ReadAsStringAsync();
                        List<Prestamo> prestamos = JsonConvert.DeserializeObject<List<Prestamo>>(jsonPrestamos);

                        // Obtener las cuentas del usuario
                        HttpResponseMessage cuentasResponse = await _httpClient.GetAsync($"{_baseURL}/{userId}");

                        if (cuentasResponse.IsSuccessStatusCode)
                        {
                            string cuentasJson = await cuentasResponse.Content.ReadAsStringAsync();
                            List<Cuenta> cuentas = JsonConvert.DeserializeObject<List<Cuenta>>(cuentasJson);

                            // Agregar las cuentas a ViewBag.Cuentas
                            ViewBag.Cuentas = cuentas;
                        }
                        else
                        {
                            _notyf.Error("No se pudieron obtener las cuentas del usuario");
                            return RedirectToAction("Index", "Home");
                        }

                        // Agregar los préstamos a ViewBag.Prestamos
                        ViewBag.Prestamos = prestamos;

                        // Utilizar los préstamos y cuentas obtenidos
                        return View(prestamos); // Pasar los préstamos a la vista
                    }
                    else
                    {
                        _notyf.Error("No se pudieron obtener los préstamos del usuario");
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
                _notyf.Error($"Error al obtener los préstamos del usuario: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Index(int cuentaOrigen, int prestamo, double monto)
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserID");
                var userRl = HttpContext.Session.GetString("UserRL");

                // Verificar si el usuario ya inició sesión
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userRl))
                    return RedirectToAction("Index", "PaginaPrincipal");

                var userDc = HttpContext.Session.GetString("UserDC");

                // Obtener la información del préstamo seleccionado
                Prestamo prestamoSeleccionado = null;
                foreach (var p in ViewBag.Prestamos)
                {
                    if (p.IdPrestamo == prestamo)
                    {
                        prestamoSeleccionado = p;
                        break;
                    }
                }

                if (prestamoSeleccionado != null)
                {
                    // Obtener el número de cuenta asociado al préstamo
                    int numCuenta = (int)prestamoSeleccionado.IdCliente;

                    // Construir el objeto de pago de préstamo
                    var pagoPrestamo = new
                    {
                        idCuenta = cuentaOrigen,
                        idPrestamo = prestamo,
                        monto = monto
                    };

                    // Convertir el objeto de pago de préstamo a JSON
                    var jsonPagoPrestamo = JsonConvert.SerializeObject(pagoPrestamo);

                    // Enviar la solicitud HTTP POST para realizar el pago de préstamo
                    HttpResponseMessage response = await _httpClient.PostAsync($"{_baseURL}/api/cash/PagoPrestamoWeb", new StringContent(jsonPagoPrestamo, System.Text.Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        _notyf.Success("Pago de préstamo realizado exitosamente");
                        return RedirectToAction("Index", "PaginaPrincipal");
                    }
                    else
                    {
                        _notyf.Error("No se pudo completar el pago de préstamo");
                        return RedirectToAction("Index", "PaginaPrincipal");
                    }
                }
                else
                {
                    _notyf.Error("No se pudo encontrar el préstamo seleccionado");
                    return RedirectToAction("Index", "PaginaPrincipal");
                }
            }
            catch (Exception ex)
            {
                _notyf.Error($"Error al realizar el pago de préstamo: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

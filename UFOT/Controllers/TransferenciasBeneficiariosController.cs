using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
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
                var userRl = HttpContext.Session.GetString("UserRL");

                // Verificar si el usuario ya inició sesión
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userRl))
                {
                    var userDc = HttpContext.Session.GetString("UserDC");
                    HttpResponseMessage response = await _httpClient.GetAsync($"{_baseURL}/api/Client/Beneficiarios/{userDc}");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<Beneficiario> beneficiarios = JsonConvert.DeserializeObject<List<Beneficiario>>(json);

                        // Obtener cuentas del usuario
                        response = await _httpClient.GetAsync($"{_baseURL}/{userDc}");

                        if (response.IsSuccessStatusCode)
                        {
                            json = await response.Content.ReadAsStringAsync();
                            List<Cuenta> cuentas = JsonConvert.DeserializeObject<List<Cuenta>>(json);

                            // Agregar las cuentas y beneficiarios a ViewBag
                            ViewBag.Cuentas = cuentas;
                            ViewBag.Beneficiarios = beneficiarios;

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
                        _notyf.Error("No se pudieron obtener los beneficiarios del usuario");
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
                _notyf.Error($"Error al obtener los beneficiarios del usuario: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

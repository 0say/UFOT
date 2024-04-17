using AspNetCoreHero.ToastNotification.Abstractions;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using UFOT.Data;
using UFOT.Models;

namespace UFOT.Controllers
{
    public class MovimientoController : BaseController
    {
        string _baseURL = "https://bankintegrationlayer20240414102922.azurewebsites.net/";

        public MovimientoController(LogService logger, BancoWebContext context, INotyfService notyf, HttpClient httpClient) : base(logger, context, notyf, httpClient)
        {
        }

        // Acción para mostrar la vista de movimientos
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserID");
            // Verificar si el usuario ya inició sesión
            if (!string.IsNullOrEmpty(userId))
            {
                var userDc = HttpContext.Session.GetString("UserDC");

                // Obtener cuentas del usuario

                //HttpResponseMessage response = await _httpClient.GetAsync("https://bankintegrationlayer20240414102922.azurewebsites.net/api/client/movimientos");
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://bankintegrationlayer20240414102922.azurewebsites.net/api/client/movimientos");
                request.Headers.Add("Cookie", "ARRAffinity=3a1d11e59b9fe6572282cdff3da0bc0976af434201bd48d6c261ead02e42eb64; ARRAffinitySameSite=3a1d11e59b9fe6572282cdff3da0bc0976af434201bd48d6c261ead02e42eb64");
                Cliente cliente = new Cliente();
                cliente.idcliente = userDc.ToString();


                var content = new StringContent(JsonConvert.SerializeObject(cliente), null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);



                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Movimiento> movimientos = JsonConvert.DeserializeObject<List<Movimiento>>(json);

                    return View(movimientos);
                };
            }
            return View();

        }
    }
}

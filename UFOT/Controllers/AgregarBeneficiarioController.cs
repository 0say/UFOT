using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UFOT.Data;
using UFOT.Models;
using System;

namespace UFOT.Controllers
{
    public class AgregarBeneficiarioController : BaseController
    {
        public AgregarBeneficiarioController(LogService logger, BancoWebContext context, INotyfService notyf, HttpClient httpClient) : base(logger, context, notyf, httpClient)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Obtener la lista de bancos de la base de datos
            var bancos = _context.Bancos.ToList();

            // Convertir la lista de bancos en una lista de SelectListItem
            var bancosSelectList = bancos.Select(b => new SelectListItem { Value = b.BancoId.ToString(), Text = b.Nombre }).ToList();

            // Agregar una opción por defecto si es necesario
            bancosSelectList.Insert(0, new SelectListItem { Value = "", Text = "Seleccione un banco" });

            // Asignar la lista de bancos al ViewBag para usarla en la vista
            ViewBag.Bancos = bancosSelectList;

            // Crear un nuevo beneficiario para el formulario
            var beneficiario = new Beneficiario();

            // Devolver la vista con el nuevo beneficiario y la lista de bancos
            return View(beneficiario);
        }


        [HttpPost]
        public IActionResult Index(string nombre, string numeroCuenta, int bancoId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar si los campos requeridos están nulos o vacíos
                    if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(numeroCuenta) || bancoId == null)
                    {
                        _notyf.Error("Todos los campos son obligatorios");
                        return View();
                    }

                    // Obtener el UsuarioId de la sesión

                    var userId2 = HttpContext.Session.GetString("UserID");
                    if (userId2 != null)
                    {
                        // Convertir el UsuarioId a un entero
                        if (int.TryParse(userId2, out int usuarioId))
                        {
                            // Crear un nuevo objeto Beneficiario con los campos proporcionados
                            var beneficiario = new Beneficiario
                            {
                                Nombre = nombre,
                                NumeroCuenta = numeroCuenta,
                                UsuarioId = usuarioId,
                                BancoId = bancoId
                            };

                            // Agregar el beneficiario al contexto y guardar los cambios en la base de datos
                            _context.Beneficiarios.Add(beneficiario);
                            _context.SaveChanges();
                            _notyf.Success($"Beneficiario {beneficiario.Nombre} agregado");

                            // Redirigir a la página principal o a alguna otra página después de agregar el beneficiario
                            return View();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _notyf.Error($"Error al agregar beneficiario: {ex.Message}");
            }

            // Si hay algún error o el modelo no es válido, volver a mostrar el formulario con los datos ingresados por el usuario
            return View();
        }

    }
}

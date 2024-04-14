using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UFOT.Data;

namespace UFOT.Controllers
{
    public class BaseController : Controller
    {
        public readonly BancoWebContext _context;
        public readonly INotyfService _notyf;
        public readonly LogService _logger;
        public readonly HttpClient _httpClient;



        public BaseController(LogService logger, BancoWebContext context, INotyfService notyf, HttpClient httpClient)
        {
            _logger = logger;
            _context = context;
            _notyf = notyf;
            _httpClient = httpClient;
        }
      
    }
}

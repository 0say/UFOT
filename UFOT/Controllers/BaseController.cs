using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using UFOT.Data;

namespace UFOT.Controllers
{
    public class BaseController : Controller
    {
        public readonly BancoWebContext _context;
        public readonly INotyfService _notyf;
        public readonly LogService _logger;



        public BaseController(LogService logger, BancoWebContext context, INotyfService notyf)
        {
            _logger = logger;
            _context = context;
            _notyf = notyf;
        }
      
    }
}

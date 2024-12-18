using System.Diagnostics;
using fomezero.Models;
using Microsoft.AspNetCore.Mvc;

namespace fomezero.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Obt�m o TipoUsuarioId da sess�o
            var tipoUsuarioId = HttpContext.Session.GetString("TipoUsuarioId");
            ViewData["TipoUsuarioId"] = tipoUsuarioId;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

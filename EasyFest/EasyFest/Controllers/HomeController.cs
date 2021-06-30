using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyFest.Models;
using Storage;

namespace EasyFest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStorageService _storage;

        public HomeController(ILogger<HomeController> logger,
                                IStorageService storage)
        {
            _logger = logger;
            _storage = storage;
        }

        public IActionResult Index()
        {
            var festivals = _storage.GetAllFestivals();
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

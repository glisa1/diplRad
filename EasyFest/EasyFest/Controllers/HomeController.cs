using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyFest.Models;
using Storage;
using Storage.Services.CommentsService;
using Storage.Services.FestivalService;
using Storage.Services.FestivalLocationsService;
using Storage.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace EasyFest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IStorageService _storage;
        private readonly IFestivalService _festivalService;
        private readonly IFestivalLocationsService _festivalLocationsService;
        private readonly ICommentsService _commentsService;
        private readonly IUserService _userService;


        public HomeController(ILogger<HomeController> logger,
                                IFestivalService festivalService,
                                IFestivalLocationsService festivallocationsService,
                                ICommentsService commentsService,
                                IUserService userService)
        {
            _logger = logger;
            _festivalService = festivalService;
            _festivalLocationsService = festivallocationsService;
            _commentsService = commentsService;
            _userService = userService;
            //_storage = storage;
        }

        public IActionResult Index()
        {
            var festivals = _festivalService.GetAllFestivals();
            var locations = _festivalLocationsService.GetAllFestivalLocations();
            
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

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EasyFest.Models;
using System.Threading.Tasks;
using System.Threading;
using GraphQL.Types;
using GraphQLDataAccess.Schema.Types;
using GraphQLDataAccess.Schema;
using EasyFest.Factories;

namespace EasyFest.Controllers
{
    public class HomeController : Controller
    {
        #region Init

        private readonly IHttpClientFactory _client;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            //FestivalFactory fact = new FestivalFactory();
            //await fact.PrepareFestival();

            var festivals = await _client.QueryGet<FestivalList>(GraphQLCommModel.QueryFestival);

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

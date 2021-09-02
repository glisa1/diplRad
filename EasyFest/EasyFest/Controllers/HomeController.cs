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

            var festivals = await _client.QueryGet<FestivalPaginate>(GraphQLCommModel.QueryFestival);

            return View(festivals);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string term)
        {
            TempData.Remove("searchValue");
            TempData.Add("searchValue", term);

            string termUpper = term.ToUpper();

            var query = GraphQLCommModel.QueryFestivalSearch
                            .Replace("{0}", term)
                            .Replace("{1}", termUpper)
                            .Replace("{2}", term)
                            .Replace("{3}", termUpper);
            var festivals = await _client.QueryGet<FestivalPaginate>(query);

            return View("Index", festivals);
        }

        public IActionResult Image(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return null;
            }

            var image = System.IO.File.OpenRead("..\\EasyFest\\Images\\" + imageName + ".jpg");
            return File(image, "image/jpeg");
        }

        public IActionResult ProfileImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return null;
            }

            var image = System.IO.File.OpenRead("..\\EasyFest\\Images\\ProfileImages\\" + imageName + ".jpg");
            return File(image, "image/jpeg");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

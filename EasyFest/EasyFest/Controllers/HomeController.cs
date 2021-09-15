using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EasyFest.Models;
using System.Threading.Tasks;
using System.Threading;
using GraphQL.Types;
using GraphQLDataAccess.Schema.Types;
using GraphQLDataAccess.Schema;
using EasyFest.Factories;
using System;

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
        
        public async Task<IActionResult> Settings()
        {
            //QuerySettingsPage
            //Zabraniti neulogovanom korisniku ulazak
            var model = await _client.QueryGet<SettingsPage>(GraphQLCommModel.QuerySettingsPage);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewTag(string TagName, string TagColor)
        {
            var mutationString = GraphQLCommModel.MutationCreateTag
            .Replace("{0}", TagName)
            .Replace("{1}", TagColor)
            .Replace("{2}", "newTag");

            var model = await _client.QueryGet<SettingsPage>(mutationString);
            if (model.Errors != null)
            {
                if (model.Errors[0].Extensions.Code == "TAKEN_Name")
                {
                    return Json(new { code = 400, status = "ERROR", message = "Tag with same name already exists!" });
                }

                return Json(new { code = 400, status = "ERROR" });
            }
            else
            {
                return Json(new { code = 200 });
            }
        }

        public IActionResult DeleteTag(string tagId)
        {
            return Ok();
        }
    }
}

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
using System.Text;

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

            TempData.Remove("searchDefault");
            TempData.Add("searchDefault", true);

            return View(festivals);
        }

        public async Task<IActionResult> SearchByTags(string userId, string term, string lastCursorName, bool isAjax = false)
        {
            #region Get user tags 

            var query = GraphQLCommModel.QueryGetUserTagsById
                .Replace("{0}", userId);

            var userTags = await _client.QueryGet<UserById>(query);

            if (userTags.Errors != null)
            {
                return RedirectToAction("Index");
            }

            if (userTags.Data.User.SelectedTags.Count == 0)
            {
                return RedirectToAction("Index");
            }

            StringBuilder userTagsFormat = new StringBuilder();
            foreach(var tag in userTags.Data.User.SelectedTags)
            {
                userTagsFormat.Append("\"");
                userTagsFormat.Append(tag);
                userTagsFormat.Append("\",");
            }

            userTagsFormat = userTagsFormat.Remove(userTagsFormat.Length - 1, 1);

            #endregion

            query = string.Empty;
            
            query = GraphQLCommModel.QueryFestivalByTagsSearch;

            if (string.IsNullOrEmpty(term))
            {
                string wherePart = "where: {tags: {some: {in: [{0}]}}}"
                    .Replace("{0}", userTagsFormat.ToString());
                query = query.Replace("{0}", wherePart);
            }
            else
            {
                string termUpper = term.ToUpper();
                string wherePart = "where:{and: [{tags: {some: {in: [{0}]}}}, {or: [{name: {contains: \"{1}\"}}, {name: {contains: \"{2}\"}}, {description: {contains: \"{3}\"}}, {description: {contains: \"{4}\"}}]}]}";
                wherePart = wherePart
                    .Replace("{0}", userTagsFormat.ToString())
                    .Replace("{1}", term)
                    .Replace("{2}", termUpper)
                    .Replace("{3}", term)
                    .Replace("{4}", termUpper);
                query = query.Replace("{0}", wherePart);
            }

            if (!string.IsNullOrEmpty(lastCursorName))
            {
                string afterPart = "after: \"{0}\""
                    .Replace("{0}", lastCursorName);
                query = query.Replace("{1}", afterPart);
            }
            else
            {
                query = query.Replace("{1}", string.Empty);
            }

            var festivals = await _client.QueryGet<FestivalPaginate>(query);

            TempData.Remove("searchDefault");
            TempData.Add("searchDefault", false);

            if (isAjax)
            {
                return Json(new { data = festivals.Data.FestivalNodes});
            }

            return View("Index", festivals);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string term)
        {
            TempData.Remove("searchValue");
            TempData.Add("searchValue", term);
            TempData.Remove("searchDefault");
            TempData.Add("searchDefault", true);

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

        public async Task<IActionResult> DeleteTag(string tagId)
        {
            //MutationDeleteTag
            try
            {
                var mutationString = GraphQLCommModel.MutationDeleteTag
                .Replace("{0}", tagId);

                await _client.MutationDo<SettingsPage>(mutationString);

                return Json(new { code = 200 });
            }
            catch(Exception)
            {
                return Json(new { code = 400 });
            }
        }
    }
}

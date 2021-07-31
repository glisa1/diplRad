using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyFest.Factories;
using EasyFest.Models;
using GraphQLDataAccess.Schema.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyFest.Controllers
{
    public class FestivalController : Controller
    {
        #region Init

        private readonly IHttpClientFactory _client;

        public FestivalController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory;
        }

        #endregion

        public async Task<IActionResult> FestivalDetails(string festivalId)
        {
            var queryString = GraphQLCommModel.QueryFestivalDetailsWithLocation.Replace("{0}", festivalId);
            var model = await _client.QueryGet<FestivalById>(queryString);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRate(string userId, string festivalId, double rateValue)
        {
            var mutationString = GraphQLCommModel.MutationCreateRate
            .Replace("{0}", festivalId)
            .Replace("{1}", userId)
            .Replace("{2}", rateValue.ToString());

            var result = await _client.MutationDo<CreateRateQueryModel>(mutationString);

            return Json(new { code = 200, rate = result.Data.Festival.Festival.Rate });
        }

        public async Task<ActionResult> UpdateRate(string userId, string festivalId, double rateValue)
        {
            var mutationString = GraphQLCommModel.MutationUpdateRate
            .Replace("{0}", festivalId)
            .Replace("{1}", userId)
            .Replace("{2}", rateValue.ToString());

            var result = await _client.MutationDo<UpdateRateQueryModel>(mutationString);

            if (result.Errors != null)
            {
                return Json(new { code = 400, status = "ERROR" });
            }

            return Json(new { code = 200, rate = result.Data.Festival.Festival.Rate });
        }

        public async Task<ActionResult> GetUserRateForFestival(string festivalId, string userId)
        {
            var queryString = GraphQLCommModel.QueryGetRateByUser
            .Replace("{0}", festivalId)
            .Replace("{1}", userId);

            var rateValue = await _client.QueryGet<UserRateForFestival>(queryString);

            if (rateValue.Errors != null)
            {
                return Json(new { code = 400, status = "ERROR" });
            }

            return Json(new { code = 200, status = "OK", rate = rateValue.Data.Rate });
        }

        public async Task<ActionResult> PostComment(string userId, string festivalId, string commentBody)
        {
            var mutationString = GraphQLCommModel.MutationCreateComment
            .Replace("{0}", userId)
            .Replace("{1}", festivalId)
            .Replace("{2}", commentBody);

            var result = await _client.MutationDo<UpdateRateQueryModel>(mutationString);

            if (result.Errors != null)
            {
                return Json(new { code = 400, status = "ERROR" });
            }

            return Json(new { code = 200 });
        }
    }
}
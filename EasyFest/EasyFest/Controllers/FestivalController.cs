using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyFest.Factories;
using EasyFest.Models;
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
    }
}
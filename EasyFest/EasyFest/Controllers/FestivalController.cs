using System;
using System.Collections.Generic;
using System.IO;
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

            return Json(new { code = 200, creationDate = DateTime.Now.ToString("dddd, dd MMMM yyyy") });
        }

        public async Task<IActionResult> FestivalMap()
        {
            var festivals = await _client.QueryGet<FestivalList>(GraphQLCommModel.QueryGetFestivalMap);

            return View(festivals);
        }

        public async Task<IActionResult> DeleteFestival(string festivalId, string userId)
        {
            var queryString = GraphQLCommModel.QueryGetIsUserAdmin
                .Replace("{0}", userId);

            var user = await _client.QueryGet<UserById>(queryString);

            if (user.Errors != null)
            {
                return Json(new { code = 400, message = user.Errors[0].Message, authorized = true });
            }

            if (!user.Data.User.IsAdmin)
            {
                return Json(new { code = 400, authorized = false });
            }

            queryString = GraphQLCommModel.QueryFestivalImages
                .Replace("{0}", festivalId);

            var festival = await _client.QueryGet<FestivalById>(queryString);

            if (festival.Errors != null)
            {
                return Json(new { code = 400, message = festival.Errors[0].Message, authorized = true });
            }

            var mutationString = GraphQLCommModel.MutationDeleteFestival
                .Replace("{0}", festivalId);

            var result = await _client.MutationDo<UpdateRateQueryModel>(mutationString);

            if (result.Errors != null)
            {
                return Json(new { code = 400, message = result.Errors[0].Message, authorized = true });
            }

            foreach (var image in festival.Data.Festival.Images)
            {
                System.IO.File.Delete("..\\EasyFest\\Images\\" + image + ".jpg");
            }

            return Json(new { code = 200 });
        }

        [HttpGet]
        public async Task<IActionResult> NewFestival()
        {
            if (!(bool)TempData["IsAdmin"])
                return Forbid();

            var result = await _client.QueryGet<TagsModel>(GraphQLCommModel.QueryGetTags);

            NewFestivalViewModel model = new NewFestivalViewModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                TagsList = result.Data.Tags
            };

            return View(model);
        }

        public async Task<IActionResult> NewFestival(NewFestivalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NewFestival");
            }

            foreach (var image in model.UploadedImages)
            {
                string imageName = Guid.NewGuid().ToString();
                model.Images.Add(imageName);
                using (FileStream fs = System.IO.File.Create("..\\EasyFest\\Images\\" + imageName + ".jpg"))
                {
                    await image.CopyToAsync(fs);
                }
            }

            var mutationString = GraphQLCommModel.MutationCreateFestival
                .Replace("{0}", model.Name)
                .Replace("{1}", model.StartDate.Month.ToString())
                .Replace("{2}", model.StartDate.Day.ToString())
                .Replace("{3}", model.EndDate.Month.ToString())
                .Replace("{4}", model.EndDate.Day.ToString())
                .Replace("{5}", model.Description)
                .Replace("{6}", string.Join(',', model.Images))
                .Replace("{7}", model.FestivalLocation.Latitude.ToString())
                .Replace("{8}", model.FestivalLocation.Longitude.ToString())
                .Replace("{9}", model.FestivalLocation.Address)
                .Replace("{10}", model.FestivalLocation.City)
                .Replace("{11}", model.FestivalLocation.State);

            var result = await _client.MutationDo<UpdateRateQueryModel>(mutationString);

            if (result.Errors != null)
            {
                return Json(new { code = 400, message = result.Errors[0].Message, authorized = true });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFestival(string festivalId)
        {
            if (!(bool)TempData["IsAdmin"])
                return Forbid();

            var queryString = GraphQLCommModel.QueryGetFestivalForEdit.Replace("{0}", festivalId);
            var data = await _client.QueryGet<FestivalWithTags>(queryString);

            if (data.Errors != null)
            {
                return Json(new { code = 400, message = data.Errors[0].Message, authorized = true });
            }

            var festival = data.Data.Festival;

            UpdateFestivalViewModel model = new UpdateFestivalViewModel
            {
                Description = festival.Description,
                EndDate = new DateTime(DateTime.Now.Year, festival.EndMonth, festival.EndDay),
                Id = festivalId,
                Name = festival.Name,
                OldName = festival.Name,
                Images = festival.Images,
                StartDate = new DateTime(DateTime.Now.Year, festival.Month, festival.Day),
                FestivalLocation = new FestivalLocationViewModel
                {
                    Address = festival.Address,
                    City = festival.City,
                    Latitude = festival.Latitude,
                    Longitude = festival.Longitude,
                    State = festival.State
                }
            };

            foreach(var tag in festival.Tags)
            {
                model.SelectedTags.Add(tag.Id);
            }

            model.TagsList = data.Data.Tags;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFestival(UpdateFestivalViewModel model)
        {
            if (!(bool)TempData["IsAdmin"])
                return Forbid();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("NewFestival");
            }

            var mutationString = string.Empty;

            if (model.Name == model.OldName)
            {
                mutationString = GraphQLCommModel.MutationUpdateFestival
                    .Replace("{0}", model.Name)
                    .Replace("{1}", model.StartDate.Month.ToString())
                    .Replace("{2}", model.StartDate.Day.ToString())
                    .Replace("{3}", model.EndDate.Month.ToString())
                    .Replace("{4}", model.EndDate.Day.ToString())
                    .Replace("{5}", model.Description)
                    .Replace("{6}", string.Join(',', model.Images))
                    .Replace("{7}", model.FestivalLocation.Latitude.ToString())
                    .Replace("{8}", model.FestivalLocation.Longitude.ToString())
                    .Replace("{9}", model.FestivalLocation.Address)
                    .Replace("{10}", model.FestivalLocation.City)
                    .Replace("{11}", model.FestivalLocation.State)
                    .Replace("{12}", string.Empty)
                    .Replace("{13}", model.Id)
                    .Replace("{14}", 0.ToString());
            }
            else
            {
                mutationString = GraphQLCommModel.MutationUpdateFestival
                    .Replace("{0}", model.Name)
                    .Replace("{1}", model.StartDate.Month.ToString())
                    .Replace("{2}", model.StartDate.Day.ToString())
                    .Replace("{3}", model.EndDate.Month.ToString())
                    .Replace("{4}", model.EndDate.Day.ToString())
                    .Replace("{5}", model.Description)
                    .Replace("{6}", string.Join(',', model.Images))
                    .Replace("{7}", model.FestivalLocation.Latitude.ToString())
                    .Replace("{8}", model.FestivalLocation.Longitude.ToString())
                    .Replace("{9}", model.FestivalLocation.Address)
                    .Replace("{10}", model.FestivalLocation.City)
                    .Replace("{11}", model.FestivalLocation.State)
                    .Replace("{12}", string.Empty)
                    .Replace("{13}", model.Id)
                    .Replace("{14}", 1.ToString());
            }

            var result = await _client.MutationDo<UpdateRateQueryModel>(mutationString);

            if (result.Errors != null)
            {
                return Json(new { code = 400, message = result.Errors[0].Message, authorized = true });
            }

            // Only if new picture was uploaded we copy it.
            if (model.UploadedImages.Any())
            {
                foreach (var image in model.UploadedImages)
                {
                    string imageName = Guid.NewGuid().ToString();
                    using (FileStream fs = System.IO.File.Create("..\\EasyFest\\Images\\" + imageName + ".jpg"))
                    {
                        await image.CopyToAsync(fs);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetPin()
        {
            var image = System.IO.File.OpenRead(".\\wwwroot\\locPin.png");
            return File(image, "image/jpeg");
        }

        public IActionResult GetShadow()
        {
            var image = System.IO.File.OpenRead(".\\wwwroot\\locPinSh.png");
            return File(image, "image/png");
        }
    }
}
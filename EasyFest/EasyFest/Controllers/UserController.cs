using System;
using System.IO;
using System.Threading.Tasks;
using EasyFest.Factories;
using EasyFest.Models;
using GraphQLDataAccess.Schema.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Services.AuthenticationService;

namespace EasyFest.Controllers
{
    public class UserController : Controller
    {
        #region Init

        private readonly IAuthenticationService _authService;
        private readonly IHttpClientFactory _client;

        public UserController(IAuthenticationService authenticationService,
                                IHttpClientFactory httpClientFactory)
        {
            _authService = authenticationService;
            _client = httpClientFactory;
        }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult Login()
        {
            TempData.Add("hasError", false);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            var mutationString = GraphQLCommModel.MutationLogInUser
                .Replace("{0}", model.Username)
                .Replace("{1}", model.Password);

            var result = await _client.MutationDo<LoginInput>(mutationString);

            if (result.Errors != null)
            {
                TempData.Add("hasError", true);
                TempData.Add("errorMessage", result.Errors[0].Message);
                return View("Login");
            }

            await _authService.SignInAsync(new Storage.Models.User { Username = model.Username }, true);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var tags = await _client.QueryGet<TagsModel>(GraphQLCommModel.QueryGetTags);

            if  (tags.Errors != null)
            {
                TempData.Add("hasError", true);
                return View();
            }

            UserRegisterModel model = new UserRegisterModel
            {
                AllTags = tags.Data.Tags
            };

            TempData.Add("hasError", false);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register");
            }

            string imageName = Guid.NewGuid().ToString();
            model.ImageId = imageName;
            using (FileStream fs = System.IO.File.Create("..\\EasyFest\\Images\\ProfileImages\\" + imageName + ".jpg"))
            {
                await model.UploadedImage.CopyToAsync(fs);
            }

            var mutationString = GraphQLCommModel.MutationCreateUser
                .Replace("{0}", model.Username)
                .Replace("{1}", model.Password)
                .Replace("{2}", model.Email)
                .Replace("{3}", string.Join(',', model.SelectedTags))
                .Replace("{4}", imageName);

            var result = await _client.MutationDo<LoginInput>(mutationString);

            if (result.Errors != null)
            {
                TempData.Add("hasError", true);
                TempData.Add("errorMessage", result.Errors[0].Message);
                return View("Register");
            }

            await _authService.SignInAsync(new Storage.Models.User { Username = model.Username }, true);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile(string userId)
        {
            var queryString = GraphQLCommModel.QueryMyProfile.Replace("{0}", userId);
            var model = await _client.QueryGet<MyProfileModel>(queryString);
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            await _authService.SignOutAsync();

            var mutation = GraphQLCommModel.MutationDeleteUser.Replace("{0}", userId);
            var result = await _client.QueryGet<DeleteUserPayload>(mutation);

            return Json(new { code = 200 });
        }

        [HttpPost]
        public async Task<IActionResult> EditProfileImage(IFormFile fileBytes, string imageGuid)
        {
            try
            {
                using (FileStream fs = System.IO.File.Create("..\\EasyFest\\Images\\ProfileImages\\" + imageGuid + ".jpg"))
                {
                    await fileBytes.CopyToAsync(fs);
                }

                return Json(new { code = 200 });
            }
            catch(Exception)
            {
                return Json(new { code = 500 });
            }
        }

        public async Task<IActionResult> AddTagToUser(string userId, string tagId)
        {
            var mutation = GraphQLCommModel
                .MutationAddTagToUser
                .Replace("{0}", tagId)
                .Replace("{1}", userId);
            var result = await _client.QueryGet<DeleteUserPayload>(mutation);

            if (result.Errors != null)
            {
                return Json(new { code = 400, status = "ERROR" });
            }

            return Json(new { code = 200 });
        }

        public async Task<IActionResult> RemoveTagFromUser(string userId, string tagId)
        {
            var mutation = GraphQLCommModel
                .MutationRemoveTagFromUser
                .Replace("{0}", tagId)
                .Replace("{1}", userId);
            var result = await _client.QueryGet<DeleteUserPayload>(mutation);

            if (result.Errors != null)
            {
                return Json(new { code = 400, status = "ERROR" });
            }

            return Json(new { code = 200 });
        }

        public async Task<IActionResult> CheckIfUserFollows(string userId, string festivalId)
        {
            var mutation = GraphQLCommModel
                .MutationCheckIfUserFollows
                .Replace("{0}", userId)
                .Replace("{1}", festivalId);
            var result = await _client.MutationDo<UserFollowsFestivalCheckModel>(mutation);

            if (result.Errors != null)
            {
                return Json(new { code = 400 });
            }

            if (result.Data.Follows)
            {
                return Json(new { code = 200, follows = true });
            }
            else
            {
                return Json(new { code = 200, follows = false });
            }
        }

        public async Task<IActionResult> FollowFestival(string userId, string festivalId)
        {
            var mutation = GraphQLCommModel
                .MutationUserFollowFest
                .Replace("{0}", userId)
                .Replace("{1}", festivalId);
            var result = await _client.QueryGet<DeleteUserPayload>(mutation);

            if (result.Errors != null)
            {
                return Json(new { code = 400 });
            }

            return Json(new { code = 200 });
        }

        public async Task<IActionResult> UnfollowFestival(string userId, string festivalId)
        {
            var mutation = GraphQLCommModel
                .MutationUserUnfollowFest
                .Replace("{0}", userId)
                .Replace("{1}", festivalId);
            var result = await _client.QueryGet<DeleteUserPayload>(mutation);

            if (result.Errors != null)
            {
                return Json(new { code = 400 });
            }

            return Json(new { code = 200 });
        }

        #endregion

    }
}
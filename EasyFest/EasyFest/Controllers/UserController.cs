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
        public IActionResult Register()
        {
            TempData.Add("hasError", false);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register");
            }

            var mutationString = GraphQLCommModel.MutationCreateUser
                .Replace("{0}", model.Username)
                .Replace("{1}", model.Password)
                .Replace("{2}", model.Email);

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
            var queryString = GraphQLCommModel.QueryGetUserById.Replace("{0}", userId);
            var model = await _client.QueryGet<UserById>(queryString);

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

        #endregion

    }
}
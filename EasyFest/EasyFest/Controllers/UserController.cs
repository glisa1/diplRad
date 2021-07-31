using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyFest.Factories;
using EasyFest.Models;
using GraphQLDataAccess.Schema.Models;
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

        #endregion

    }
}
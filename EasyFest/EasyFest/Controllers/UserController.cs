using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Services.AuthenticationService;

namespace EasyFest.Controllers
{
    public class UserController : Controller
    {
        #region Init

        private readonly IAuthenticationService _authService;

        public UserController(IAuthenticationService authenticationService)
        {
            _authService = authenticationService;
        }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(int testId)
        {
            User logUser = new User
            {
                Email = "test@mail.com",
                Password = "testPwd",
                Username = "testUsrname",
                Id = "1"
            };

            _authService.SignIn(logUser, true);

            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        #endregion

    }
}
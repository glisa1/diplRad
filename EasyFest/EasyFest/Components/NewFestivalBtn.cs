using Microsoft.AspNetCore.Mvc;
using Storage.Services.AuthenticationService;
using EasyFest.Models;
using System.Threading.Tasks;

namespace EasyFest.Components
{
    public class NewFestivalBtn : ViewComponent
    {
        #region Init

        private readonly IAuthenticationService _authService;

        public NewFestivalBtn(IAuthenticationService authService)
        {
            _authService = authService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _authService.GetAuthenticatedCustomer();
            //var items = await GetItemsAsync(maxPriority, isDone);
            if (user != null)
                return View(new User { Email = user.Email, Id = user.Id, Username = user.Username, IsAdmin = user.IsAdmin });
            else
                return View(new User());
        }
    }
}

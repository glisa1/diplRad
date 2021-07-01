using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Services.AuthenticationService
{
    public class FestAuthenticationDefaults
    {
        public static string ClaimsIssuer => "easyFest";

        public static string AuthenticationScheme => "Authentication";

        public static PathString LoginPath => new PathString("/login");

        public static PathString LogoutPath => new PathString("/logout");

        public static PathString AccessDeniedPath => new PathString("/page-not-found");

        public static string AuthenticationCookie => ".Authentication";

        public static string Prefix => ".EasyFest";
    }
}

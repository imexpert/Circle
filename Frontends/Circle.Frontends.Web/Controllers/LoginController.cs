using Circle.Core.Extensions;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IStringLocalizer<LoginController> _localizer;

        IAuthService _authService;
        IHttpContextAccessor _httpContextAccessor;

        public LoginController(IAuthService authService, 
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<LoginController> localizer)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            string aaa = _localizer["LoginTitle"].Value;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return Json(await _authService.LoginAsync(loginModel));
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}

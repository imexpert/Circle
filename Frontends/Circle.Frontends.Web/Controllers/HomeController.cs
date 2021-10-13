using Circle.Frontends.Web.Models;
using Circle.Frontends.Web.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Controllers
{
    
    public class HomeController : BaseController
    {
        IStringLocalizer<HomeController> _localizer;
        IHttpContextAccessor _httpContextAccessor;
        IGroupService _groupService;

        public HomeController(IHttpContextAccessor httpContextAccessor, IStringLocalizer<HomeController> localizer)
        {
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage2(LanguageModel model)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(model.Culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(model.ReturnUrl);
        }
    }
}

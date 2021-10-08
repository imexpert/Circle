using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Controllers
{
    public class LoginController : Controller
    {
        IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return Json(await _authService.LoginAsync(loginModel));
        }
    }
}

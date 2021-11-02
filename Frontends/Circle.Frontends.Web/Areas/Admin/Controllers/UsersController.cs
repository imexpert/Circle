using Circle.Frontends.Web.Controllers;
using Circle.Frontends.Web.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : BaseController
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> List()
        {
            var userResponse = await _userService.GetList();
            return View(userResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var userResponse = await _userService.GetList();
            return Json(userResponse);
        }
    }
}

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

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var userResponse = await _userService.GetList();
            return Json(userResponse);
        }
    }
}

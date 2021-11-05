using Circle.Frontends.Web.Controllers;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : BaseController
    {
        IUserService _userService;
        IDepartmentService _departmentService;
        IGroupService _groupService;

        public UsersController(
            IUserService userService,
            IDepartmentService departmentService,
            IGroupService groupService)
        {
            _userService = userService;
            _departmentService = departmentService;
            _groupService = groupService;
        }

        public async Task<IActionResult> List()
        {
            UserListModel model = new UserListModel();

            var userResponse = await _userService.GetList();
            var departmentResponse = await _departmentService.GetList();
            var groupResponse = await _groupService.GetList();

            model.UserList = userResponse.Data;
            model.DepartmentList = departmentResponse.Data;
            model.GroupList = groupResponse.Data;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var userResponse = await _userService.GetList();
            return Json(userResponse);
        }
    }
}

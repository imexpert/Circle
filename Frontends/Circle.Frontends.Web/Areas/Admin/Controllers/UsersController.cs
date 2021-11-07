using Circle.Frontends.Web.Controllers;
using Circle.Frontends.Web.Infrastructure.Extensions;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserModel model)
        {
            int dosyaSayisi = HttpContext.Request.Form.Files.Count;
            if (dosyaSayisi > 0)
            {
                model.Image = await HttpContext.Request.Form.Files[0].GetBytes();
            }
            var userResponse = await _userService.AddAsync(model);
            return Json(userResponse);
        }
    }
}

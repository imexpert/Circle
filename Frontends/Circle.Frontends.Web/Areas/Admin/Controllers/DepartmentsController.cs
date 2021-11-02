using Circle.Frontends.Web.Controllers;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentsController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> List()
        {
            var departmentResponse = await _departmentService.GetList();
            return View(departmentResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentList()
        {
            var departmentResponse = await _departmentService.GetList();
            return Json(departmentResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(CreateDepartmentModel model)
        {
            var departmentResponse = await _departmentService.AddAsync(model);
            return Json(departmentResponse);
        }
    }
}

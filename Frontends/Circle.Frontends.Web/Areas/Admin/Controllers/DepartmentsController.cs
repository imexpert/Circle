﻿using Circle.Frontends.Web.Controllers;
using Circle.Frontends.Web.Services.Abstract;
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
    }
}

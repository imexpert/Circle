using Circle.Core.Entities.Concrete;
using Circle.Frontends.Web.Controllers;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : BaseController
    {
        ICategoryService _categoryService;

        public ProductsController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult UniqueCode()
        {
            return View();
        }

        public async Task<IActionResult> CreateCode(Guid guid)
        {
            var categoryList = new CategoryListModel();

            var categoryResponse = await _categoryService.GetAllSubCategories(guid);
            if (categoryResponse.IsSuccess)
            {
                categoryList = categoryResponse.Data;
            }

            return View(categoryList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct()
        {
            return Json(null);
        }
    }
}

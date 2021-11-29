using Circle.Core.Entities.Concrete;
using Circle.Frontends.Web.Controllers;
using Circle.Frontends.Web.Infrastructure.Extensions;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Entities.Concrete;
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
        IProductService _productService;

        public ProductsController(
            ICategoryService categoryService,
            IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public IActionResult UniqueCode()
        {
            return View();
        }

        public async Task<IActionResult> Product(Guid productId)
        {
            ProductModel product = new ProductModel();

            var response = await _productService.GetAsync(productId);
            if (response != null && response.IsSuccess)
            {
                product = response.Data;
            }
            return View(product);
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
        public async Task<IActionResult> CreateProduct(CategoryListModel model)
        {
            Product product = new Product()
            {
                CategoryId = model.SelectedCategory,
                Name = model.ProductName
            };

            var response = await _productService.AddAsync(product);
            if (response.IsSuccess)
            {
                return RedirectToAction("Product", new { productId = response.Data.Id });
            }

            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProuctModel model)
        {
            int dosyaSayisi = HttpContext.Request.Form.Files.Count;
            if (dosyaSayisi > 0)
            {
                model.Image = await HttpContext.Request.Form.Files[0].GetBytes();
            }

            if (HttpContext.Request.Form.ContainsKey("ProductDescription"))
            {
                model.ProductDescription = HttpContext.Request.Form["ProductDescription"][1];
            }
            
            var userResponse = await _productService.UpdateAsync(model);
            return Json(userResponse);
        }
    }
}

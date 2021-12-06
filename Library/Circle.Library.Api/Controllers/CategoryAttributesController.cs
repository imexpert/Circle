using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Handlers.Categories.Commands;
using Circle.Library.Business.Handlers.Categories.Queries;
using Circle.Library.Business.Handlers.CategoryAttributes.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Circle.Library.Api.Controllers
{
    [ApiController]
    public class CategoryAttributesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAttributes(Guid categoryId)
        {
            return CreateActionResultInstance(await Mediator.Send(new GetCategoryAttributesQuery { CategoryId = categoryId }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMaterials(Guid productId)
        {
            return CreateActionResultInstance(await Mediator.Send(new GetMaterialsQuery { ProductId = productId }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMaterialDetails(Guid materialId)
        {
            return CreateActionResultInstance(await Mediator.Send(new GetMaterialDetailsQuery { MaterialId = materialId }));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDiameters(Guid productId)
        {
            return CreateActionResultInstance(await Mediator.Send(new GetDiametersQuery { ProductId = productId }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLengths(Guid productId)
        {
            return CreateActionResultInstance(await Mediator.Send(new GetLengthsQuery { ProductId = productId }));
        }
    }
}

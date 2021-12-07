using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Handlers.Categories.Commands;
using Circle.Library.Business.Handlers.Categories.Queries;
using Circle.Library.Business.Handlers.Groups.Commands;
using Circle.Library.Business.Handlers.ProductDetails.Commands;
using Circle.Library.Business.Handlers.ProductDetails.Queries;
using Circle.Library.Business.Handlers.Products.Queries;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Circle.Library.Api.Controllers
{
    [ApiController]
    public class ProductDetailsController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productDetail"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDetail productDetail)
        {
            return CreateActionResultInstance(await Mediator.Send(new CreateProductDetailCommand() { Model = productDetail }));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList(Guid productId)
        {
            return CreateActionResultInstance(await Mediator.Send(new GetProductDetailsQuery { ProductId = productId }));
        }

        
    }
}

using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Handlers.Categories.Commands;
using Circle.Library.Business.Handlers.Categories.Queries;
using Circle.Library.Business.Handlers.Groups.Commands;
using Circle.Library.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Circle.Library.Api.Controllers
{
    [ApiController]
    public class ProductsController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            return CreateActionResultInstance(await Mediator.Send(new CreateProductCommand() { Model = product }));
        }
    }
}

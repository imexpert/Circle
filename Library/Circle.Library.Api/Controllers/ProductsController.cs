using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Handlers.Categories.Commands;
using Circle.Library.Business.Handlers.Categories.Queries;

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
        /// Add Category.
        /// </summary>
        /// <param name="createCategory"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryCommand createCategory)
        {
            return CreateActionResultInstance(await Mediator.Send(createCategory));
        }
    }
}

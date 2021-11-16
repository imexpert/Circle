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
    public class CategoriesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetCategoriesQuery command = new GetCategoriesQuery();

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSubCategories(Guid categoryId)
        {
            return CreateActionResultInstance(await Mediator.Send(new GetSubCategoriesQuery { Id = categoryId }));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>Category List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(Guid categoryId)
        {
            return CreateActionResultInstance(await Mediator.Send(new GetCategoryQuery { Id = categoryId }));
        }

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

        /// <summary>
        /// Update Category.
        /// </summary>
        /// <param name="updateCategory"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand updateCategory)
        {
            return CreateActionResultInstance(await Mediator.Send(updateCategory));
        }

        /// <summary>
        /// Delete Category.
        /// </summary>
        /// <param name="deleteCategory"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand deleteCategory)
        {
            return CreateActionResultInstance(await Mediator.Send(deleteCategory));
        }
    }
}

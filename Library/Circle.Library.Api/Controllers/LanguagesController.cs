using System;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Handlers.Languages.Commands;
using Circle.Library.Business.Handlers.Languages.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Circle.Library.Api.Controllers
{
    public class LanguagesController : BaseApiController
    {
        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>Language List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Language))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(Guid languageId)
        {
            return CreateActionResultInstance(await Mediator.Send(new GetLanguageQuery { Id = languageId }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetByCode(string code)
        {
            return CreateActionResultInstance(await Mediator.Send(new GetLanguageByCodeQuery { Code = code }));
        }

        /// <summary>
        /// Add Language.
        /// </summary>
        /// <param name="createLanguage"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateLanguageCommand createLanguage)
        {
            return CreateActionResultInstance(await Mediator.Send(createLanguage));
        }

        /// <summary>
        /// Update Language.
        /// </summary>
        /// <param name="updateLanguage"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLanguageCommand updateLanguage)
        {
            return CreateActionResultInstance(await Mediator.Send(updateLanguage));
        }

        /// <summary>
        /// Delete Language.
        /// </summary>
        /// <param name="deleteLanguage"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteLanguageCommand deleteLanguage)
        {
            return CreateActionResultInstance(await Mediator.Send(deleteLanguage));
        }
    }
}
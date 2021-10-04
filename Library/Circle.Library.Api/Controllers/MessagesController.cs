using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Handlers.Messages.Commands;
using Circle.Library.Business.Handlers.Messages.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Circle.Library.Api.Controllers
{
    [Route("api/{culture:culture}/[controller]")]
    [ApiController]
    public class MessagesController : BaseApiController
    {
        /// <summary>
        /// Addded GroupClaim .
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Message model)
        {
            CreateMessageCommand command = new CreateMessageCommand()
            {
                Model = model
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }
    }
}

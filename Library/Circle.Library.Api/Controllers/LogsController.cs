using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Handlers.Logs.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Circle.Library.Api.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : BaseApiController
    {
        /// <summary>
        /// List Logs
        /// </summary>
        /// <remarks>bla bla bla Logs</remarks>
        /// <return>Logs List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OperationClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetLogDtoQuery()));
        }
    }
}
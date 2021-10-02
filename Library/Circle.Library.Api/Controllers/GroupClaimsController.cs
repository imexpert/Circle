using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Library.Business.Handlers.GroupClaims.Commands;
using Circle.Library.Business.Handlers.GroupClaims.Queries;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Circle.Library.Entities.ComplexTypes;

namespace Circle.Library.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    ///
    [Route("api/[controller]")]
    [ApiController]
    public class GroupClaimsController : BaseApiController
    {
        /// <summary>
        /// GroupClaims list
        /// </summary>
        /// <remarks>GroupClaims</remarks>
        /// <return>GroupClaims List</return>
        /// <response code="200"></response>
        // [AllowAnonymous]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GroupClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new GetGroupClaimsQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>GroupClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupClaim))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new GetGroupClaimQuery { Id = id }));
        }

        /// <summary>
        /// Brings up Claims by Group Id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>GroupClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getgroupclaimsbygroupid")]
        public async Task<IActionResult> GetGroupClaimsByGroupId(Guid id)
        {
            return GetResponseOnlyResultData(
                await Mediator.Send(new GetGroupClaimsLookupByGroupIdQuery { GroupId = id }));
        }

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
        public async Task<IActionResult> Add([FromBody] CreateGroupClaimModel model)
        {
            CreateGroupClaimCommand command = new CreateGroupClaimCommand()
            {
                Model = model
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// Update GroupClaim.
        /// </summary>
        /// <param name="updateGroupClaim"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGroupClaimCommand updateGroupClaim)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateGroupClaim));
        }

        /// <summary>
        /// Delete GroupClaim.
        /// </summary>
        /// <param name="deleteGroupClaim"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGroupClaimCommand deleteGroupClaim)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteGroupClaim));
        }
    }
}
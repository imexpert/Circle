﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;
using Circle.Library.Business.Handlers.UserGroups.Commands;
using Circle.Library.Business.Handlers.UserGroups.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Circle.Library.Api.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    ///
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupsController : BaseApiController
    {
        /// <summary>
        /// List UserGroup
        /// </summary>
        /// <remarks>bla bla bla UserGroups</remarks>
        /// <return>Kullanıcı Grup List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserGroupsQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyuserid")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserGroupLookupQuery { UserId = userId }));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>UserGroups List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getusergroupbyuserid")]
        public async Task<IActionResult> GetGroupClaimsByUserId(Guid id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserGroupLookupByUserIdQuery { UserId = id }));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>UserGroups List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getusersingroupbygroupid")]
        public async Task<IActionResult> GetUsersInGroupByGroupid(Guid id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUsersInGroupLookupByGroupIdQuery
                { GroupId = id }));
        }

        /// <summary>
        /// Add UserGroup.
        /// </summary>
        /// <param name="createUserGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserGroupCommand createUserGroup)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createUserGroup));
        }

        /// <summary>
        /// Update UserGroup.
        /// </summary>
        /// <param name="updateUserGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserGroupCommand updateUserGroup)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateUserGroup));
            var result = await Mediator.Send(updateUserGroup);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        /// <summary>
        /// Update UserGroup by Id.
        /// </summary>
        /// <param name="updateUserGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updatebygroupid")]
        public async Task<IActionResult> UpdateByGroupId([FromBody] UpdateUserGroupByGroupIdCommand updateUserGroup)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateUserGroup));
        }

        /// <summary>
        /// Delete UserGroup.
        /// </summary>
        /// <param name="deleteUserGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserGroupCommand deleteUserGroup)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteUserGroup));
        }
    }
}
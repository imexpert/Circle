using System;
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

    [ApiController]
    public class UserGroupsController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetUserGroupsQuery command = new GetUserGroupsQuery();

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWithId(Guid userGroupId)
        {
            GetUserGroupQuery command = new GetUserGroupQuery()
            {
                Id = userGroupId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userGroup"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]UserGroup userGroup)
        {
            CreateUserGroupCommand command = new CreateUserGroupCommand()
            {
                UserGroup = userGroup
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid userGroupId)
        {
            DeleteUserGroupCommand command = new DeleteUserGroupCommand()
            {
                Id = userGroupId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }
    }
}
using System.Threading.Tasks;
using Circle.Library.Business.Handlers.Groups.Commands;
using Circle.Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Circle.Library.Business.Handlers.Groups.Queries;
using System;
using Circle.Library.Entities.ComplexTypes;

namespace Circle.Library.Api.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    ///

    [ApiController]
    public class GroupsController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetGroupsQuery command = new GetGroupsQuery();

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grupId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWithId(Guid groupId)
        {
            GetGroupQuery command = new GetGroupQuery()
            {
                GroupId = groupId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grupId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWithClaims(Guid groupId)
        {
            GetGroupWithClaimsQuery command = new GetGroupWithClaimsQuery()
            {
                GroupId = groupId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="groupWithGroupClaimsModel"></param>
       /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(GroupModel groupModel)
        {
            CreateGroupCommand command = new CreateGroupCommand()
            {
                Model = groupModel
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GroupModel groupModel)
        {
            UpdateGroupCommand command = new UpdateGroupCommand()
            {
                Model = groupModel
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid groupId)
        {
            DeleteGroupCommand command = new DeleteGroupCommand()
            {
                Id = groupId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }
    }
}
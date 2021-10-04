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
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    ///

    [ApiController]
    public class GroupClaimsController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetGroupClaimsQuery command = new GetGroupClaimsQuery();

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupClaimId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWithId(Guid groupClaimId)
        {
            GetGroupClaimQuery command = new GetGroupClaimQuery()
            {
                Id = groupClaimId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupClaim"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]GroupClaim groupClaim)
        {
            CreateGroupClaimCommand command = new CreateGroupClaimCommand()
            {
                Model = groupClaim
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupClaimId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid groupClaimId)
        {
            DeleteGroupClaimCommand command = new DeleteGroupClaimCommand()
            {
                Id = groupClaimId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }
    }
}
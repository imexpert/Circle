using System.Threading.Tasks;
using Circle.Library.Business.Handlers.OperationClaims.Commands;
using Circle.Library.Business.Handlers.OperationClaims.Queries;
using Circle.Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Circle.Library.Api.Controllers
{
    [ApiController]
    public class OperationClaimsController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetOperationClaimsQuery command = new GetOperationClaimsQuery();

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationClaimId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWithId(Guid operationClaimId)
        {
            GetOperationClaimQuery command = new GetOperationClaimQuery()
            {
                Id = operationClaimId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationClaim"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OperationClaim operationClaim)
        {
            CreateOperationClaimCommand command = new CreateOperationClaimCommand()
            {
                Model = operationClaim
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationClaim"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OperationClaim operationClaim)
        {
            UpdateOperationClaimCommand command = new UpdateOperationClaimCommand()
            {
                Model = operationClaim
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationClaimId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid operationClaimId)
        {
            DeleteOperationClaimCommand command = new DeleteOperationClaimCommand()
            {
                Id = operationClaimId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }
    }
}
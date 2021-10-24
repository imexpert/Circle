using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Handlers.Messages.Commands;
using Circle.Library.Business.Handlers.Messages.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Circle.Library.Api.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    ///

    [ApiController]
    public class MessagesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetMessagesQuery command = new GetMessagesQuery();

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWithId(Guid messageId)
        {
            GetMessageQuery command = new GetMessageQuery()
            {
                Id = messageId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Message message)
        {
            CreateMessageCommand command = new CreateMessageCommand()
            {
                Model = message
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Message message)
        {
            UpdateMessageCommand command = new UpdateMessageCommand()
            {
                Message = message
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid messageId)
        {
            DeleteMessageCommand command = new DeleteMessageCommand()
            {
                Id = messageId
            };

            return CreateActionResultInstance(await Mediator.Send(command));
        }
    }
}

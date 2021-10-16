using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.Handlers.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Business.Helpers
{
    public class ReturnUtility : IReturnUtility
    {
        static IMediator _mediator;

        public ReturnUtility(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResponseMessage<T>> Success<T>(MessageDefinitions messageCode)
        {
            var message = await _mediator.Send(new GetMessageWithCodeQuery()
            {
                MessageCode = ((int)messageCode)
            });

            if (message == null || string.IsNullOrEmpty(message.ToString()))
            {
                message = DefaultMessageDefinitions.DefaultMessages[((int)messageCode)];
            }

            return ResponseMessage<T>.Success(message);
        }

        public async Task<ResponseMessage<T>> SuccessWithData<T>(MessageDefinitions messageCode, T data)
        {
            var message = await _mediator.Send(new GetMessageWithCodeQuery()
            {
                MessageCode = ((int)messageCode)
            });

            if (message == null || string.IsNullOrEmpty(message.ToString()))
            {
                message = DefaultMessageDefinitions.DefaultMessages[((int)messageCode)];
            }

            return ResponseMessage<T>.Success(data, message);
        }

        public ResponseMessage<T> SuccessDataTable<T>(T data, int recordsTotal, int recordsTotalFiltered)
        {
            return ResponseMessage<T>.SuccessDataTable(data, recordsTotal, recordsTotalFiltered);
        }

        public ResponseMessage<T> SuccessData<T>(T data)
        {
            return ResponseMessage<T>.Success(data);
        }

        public async Task<ResponseMessage<T>> NoDataFound<T>(MessageDefinitions messageCode)
        {
            var message = await _mediator.Send(new GetMessageWithCodeQuery()
            {
                MessageCode = ((int)messageCode)
            });

            return ResponseMessage<T>.NoDataFound(message);
        }

        public async Task<ResponseMessage<T>> Fail<T>(MessageDefinitions messageCode)
        {
            var message = await _mediator.Send(new GetMessageWithCodeQuery()
            {
                MessageCode = ((int)messageCode)
            });

            return ResponseMessage<T>.Fail(message);
        }
    }
}

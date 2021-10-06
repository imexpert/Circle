using Circle.Core.Aspects.Autofac.Transaction;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Library.Business.Handlers.Messages.Commands
{
    public class UpdateMessageCommand : IRequest<ResponseMessage<Message>>
    {
        public Message Message { get; set; }

        public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, ResponseMessage<Message>>
        {
            private readonly IMessageRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;

            public UpdateMessageCommandHandler(IMessageRepository groupRepository, IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Message>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
            {
                var message = await _groupRepository.GetAsync(s => s.Id == request.Message.Id);
                if (message == null)
                {
                    return await _returnUtility.NoDataFound<Message>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                message.LanguageId = request.Message.LanguageId;
                message.MessageDetail = request.Message.MessageDetail;
                message.Code = request.Message.Code;

                _groupRepository.Update(message);
                await _groupRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.GUNCELLEME_ISLEMI_BASARILI, message);
            }
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;

using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.UserGroups.Commands
{
    public class CreateUserGroupCommand : IRequest<ResponseMessage<UserGroup>>
    {
        public UserGroup UserGroup { get; set; }

        public class CreateUserGroupCommandHandler : IRequestHandler<CreateUserGroupCommand, ResponseMessage<UserGroup>>
        {
            private readonly IUserGroupRepository _repository;
            private readonly IReturnUtility _returnUtility;

            public CreateUserGroupCommandHandler(IUserGroupRepository repository, IReturnUtility returnUtility)
            {
                _repository = repository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<UserGroup>> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
            {
                var userGroup = await _repository.GetAsync(s => s.GroupId == request.UserGroup.GroupId && s.UserId == request.UserGroup.UserId);
                if (userGroup != null)
                {
                    return await _returnUtility.Fail<UserGroup>(MessageDefinitions.KAYIT_ZATEN_MEVCUT);
                }

                _repository.Add(request.UserGroup);
                await _repository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.KAYIT_ISLEMI_BASARILI, request.UserGroup);
            }
        }
    }
}
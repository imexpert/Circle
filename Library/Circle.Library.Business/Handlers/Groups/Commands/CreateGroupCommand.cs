using System;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Groups.Commands
{
    public class CreateGroupCommand : IRequest<ResponseMessage<Group>>
    {
        public string GroupName { get; set; }

        public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, ResponseMessage<Group>>
        {
            private readonly IGroupRepository _groupRepository;


            public CreateGroupCommandHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<ResponseMessage<Group>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
            {
                var group = new Group
                {
                    GroupName = request.GroupName
                };
                _groupRepository.Add(group);
                await _groupRepository.SaveChangesAsync();
                return ResponseMessage<Group>.Success(group);
            }
        }
    }
}
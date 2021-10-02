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

namespace Circle.Library.Business.Handlers.UserGroups.Commands
{
    public class CreateUserGroupCommand : IRequest<IResult>
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }

        public class CreateUserGroupCommandHandler : IRequestHandler<CreateUserGroupCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public CreateUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
            {
                var userGroup = new UserGroup
                {
                    GroupId = request.GroupId,
                    UserId = request.UserId
                };

                _userGroupRepository.Add(userGroup);
                await _userGroupRepository.SaveChangesAsync();

                return new SuccessResult(null);
            }
        }
    }
}
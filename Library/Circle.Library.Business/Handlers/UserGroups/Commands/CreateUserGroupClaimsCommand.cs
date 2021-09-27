using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.UserGroups.Commands
{
    public class CreateUserGroupClaimsCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
        public IEnumerable<UserGroup> UserGroups { get; set; }

        public class CreateGroupClaimsCommandHandler : IRequestHandler<CreateUserGroupClaimsCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public CreateGroupClaimsCommandHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(CreateUserGroupClaimsCommand request, CancellationToken cancellationToken)
            {
                foreach (var claim in request.UserGroups)
                {
                    _userGroupRepository.Add(new UserGroup
                    {
                        GroupId = claim.GroupId,
                        UserId = request.UserId
                    });
                }

                await _userGroupRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}
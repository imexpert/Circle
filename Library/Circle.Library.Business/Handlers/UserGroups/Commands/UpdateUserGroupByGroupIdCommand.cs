using System.Linq;
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

namespace Circle.Library.Business.Handlers.UserGroups.Commands
{
    public class UpdateUserGroupByGroupIdCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int[] UserIds { get; set; }

        public class UpdateUserGroupByGroupIdCommandHandler : IRequestHandler<UpdateUserGroupByGroupIdCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public UpdateUserGroupByGroupIdCommandHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(UpdateUserGroupByGroupIdCommand request, CancellationToken cancellationToken)
            {
                var list = request.UserIds.Select(x => new UserGroup() { GroupId = request.GroupId, UserId = x });
                await _userGroupRepository.BulkInsertByGroupId(request.GroupId, list);
                await _userGroupRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
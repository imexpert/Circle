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
    public class UpdateUserGroupCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int[] GroupId { get; set; }

        public class UpdateUserGroupCommandHandler : IRequestHandler<UpdateUserGroupCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public UpdateUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }


            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken)
            {
                var userGroupList =
                    request.GroupId.Select(x => new UserGroup() { GroupId = x, UserId = request.UserId });

                await _userGroupRepository.BulkInsert(request.UserId, userGroupList);
                await _userGroupRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
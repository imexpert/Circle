using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.UserGroups.Commands
{
    public class DeleteUserGroupCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }

        public class DeleteUserGroupCommandHandler : IRequestHandler<DeleteUserGroupCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public DeleteUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
            {
                var entityToDelete = await _userGroupRepository.GetAsync(x => x.UserId == request.Id);

                _userGroupRepository.Delete(entityToDelete);
                await _userGroupRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
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
    public class UpdateGroupCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, IResult>
        {
            private readonly IGroupRepository _groupRepository;

            public UpdateGroupCommandHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
            {
                var groupToUpdate = new Group
                {
                    Id = request.Id,
                    GroupName = request.GroupName
                };

                _groupRepository.Update(groupToUpdate);
                await _groupRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
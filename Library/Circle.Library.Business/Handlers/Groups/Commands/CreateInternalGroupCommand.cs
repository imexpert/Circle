using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Groups.Commands
{
    public class CreateInternalGroupCommand : IRequest<bool>
    {
        public Group Group { get; set; }

        public class CreateInternalGroupCommandHandler : IRequestHandler<CreateInternalGroupCommand, bool>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;

            public CreateInternalGroupCommandHandler(IGroupRepository groupRepository, IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
            }

            public async Task<bool> Handle(CreateInternalGroupCommand request, CancellationToken cancellationToken)
            {
                _groupRepository.Add(request.Group);
                await _groupRepository.SaveChangesAsync();
                return true;
            }
        }
    }
}
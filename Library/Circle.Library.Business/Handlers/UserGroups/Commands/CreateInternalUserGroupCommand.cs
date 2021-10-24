using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.UserGroups.Commands
{
    public class CreateInternalUserGroupCommand : IRequest<bool>
    {
        public UserGroup UserGroup { get; set; }

        public class CreateInternalUserGroupCommandHandler : IRequestHandler<CreateInternalUserGroupCommand, bool>
        {
            private readonly IUserGroupRepository _repository;

            public CreateInternalUserGroupCommandHandler(IUserGroupRepository repository)
            {
                _repository = repository;
            }

            public async Task<bool> Handle(CreateInternalUserGroupCommand request, CancellationToken cancellationToken)
            {
                var exists = _repository.GetAsync(s => s.GroupId == request.UserGroup.GroupId && s.UserId == request.UserGroup.UserId);
                if (exists == null)
                {
                    _repository.Add(request.UserGroup);
                    await _repository.SaveChangesAsync();
                }
                
                return true;
            }
        }
    }
}
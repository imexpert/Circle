using System;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;
using Circle.Core.Extensions;
using Circle.Core.Utilities.Security.Hashing;
using Circle.Core.Aspects.Autofac.Transaction;
using Circle.Library.Business.Handlers.UserGroups.Commands;

namespace Circle.Library.Business.Handlers.Users.Commands
{
    public class CreateUserCommand : IRequest<ResponseMessage<CreateUserModel>>
    {
        public CreateUserModel Model { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseMessage<CreateUserModel>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserGroupRepository _userGroupRepository;
            private readonly IReturnUtility _returnUtility;
            private readonly IMediator _mediator;

            public CreateUserCommandHandler(
                IUserRepository userRepository,
                IReturnUtility returnUtility,
                IMediator mediator)
            {
                _userRepository = userRepository;
                _mediator = mediator;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            [TransactionScopeAspectAsync]
            public async Task<ResponseMessage<CreateUserModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.Email == request.Model.Email || u.MobilePhones == request.Model.MobilePhones);

                if (isThereAnyUser != null)
                {
                    return await _returnUtility.Fail<CreateUserModel>(MessageDefinitions.KAYIT_ZATEN_MEVCUT);
                }

                var user = new User
                {
                    Image = request.Model.Image,
                    Email = request.Model.Email,
                    Firstname = request.Model.Firstname,
                    Lastname = request.Model.Lastname,
                    Status = request.Model.Status,
                    Address = request.Model.Address,
                    BirthDate = request.Model.BirthDate,
                    DepartmentId = request.Model.DepartmentId,
                    Gender = request.Model.Gender,
                    Notes = request.Model.Notes,
                    MobilePhones = request.Model.MobilePhones,
                    Password = HashingHelper.CreatePassword(10)
                };

                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();

                foreach (var item in request.Model.UserGroups)
                {
                    UserGroup ug = new UserGroup();
                    ug.Id = Guid.NewGuid();
                    ug.UserId = user.Id;
                    ug.GroupId = item;

                    await _mediator.Send(new CreateUserGroupCommand() { UserGroup = ug });
                }

                return await _returnUtility.SuccessWithData(MessageDefinitions.KAYIT_ISLEMI_BASARILI, request.Model);
            }
        }
    }
}
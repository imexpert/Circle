using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Circle.Core.CrossCuttingConcerns.Caching;
using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Hashing;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Core.Aspects.Autofac.Validation;
using Circle.Library.Business.Handlers.Authorizations.ValidationRules;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.Authorizations.Queries
{
    public class LoginUserQuery : IRequest<ResponseMessage<AccessToken>>
    {
        public LoginModel LoginModel { get; set; }

        public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ResponseMessage<AccessToken>>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IMediator _mediator;
            private readonly ICacheManager _cacheManager;
            private readonly IReturnUtility _returnUtility;

            public LoginUserQueryHandler(
                IUserRepository userRepository, 
                ITokenHelper tokenHelper, 
                IMediator mediator, 
                ICacheManager cacheManager,
                IReturnUtility returnUtility)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _mediator = mediator;
                _cacheManager = cacheManager;
                _returnUtility = returnUtility;
            }

            [ValidationAspect(typeof(LoginUserValidator), Priority = 1)]
            public async Task<ResponseMessage<AccessToken>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.Email == request.LoginModel.Email && u.Status);

                if (user == null)
                {
                    return await _returnUtility.NoDataFound<AccessToken>(MessageDefinitions.KULLANICI_ADI_YADA_SIFRE_HATALI);
                }

                if (!HashingHelper.VerifyPasswordHash(request.LoginModel.Password, user.Password))
                {
                    return await _returnUtility.NoDataFound<AccessToken>(MessageDefinitions.KULLANICI_ADI_YADA_SIFRE_HATALI);
                }

                var claims = _userRepository.GetClaims(user.Id);

                var accessToken = _tokenHelper.CreateToken<AccessToken>(user);
                accessToken.Claims = claims.Select(x => x.Name).ToList();

                user.RefreshToken = accessToken.RefreshToken;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                _cacheManager.Add($"{CacheKeys.UserIdForClaim}={user.Id}", claims.Select(x => x.Name));

                return _returnUtility.SuccessData(accessToken);
            }
        }
    }
}
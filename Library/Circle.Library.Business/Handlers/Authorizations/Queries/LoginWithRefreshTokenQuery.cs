using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Caching;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Authorizations.Queries
{
    public class LoginWithRefreshTokenQuery : IRequest<IResult>
    {
        public string RefreshToken { get; set; }

        public class LoginWithRefreshTokenQueryHandler : IRequestHandler<LoginWithRefreshTokenQuery, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly ICacheManager _cacheManager;

            public LoginWithRefreshTokenQueryHandler(IUserRepository userRepository, ITokenHelper tokenHelper, ICacheManager cacheManager)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _cacheManager = cacheManager;
            }

            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(LoginWithRefreshTokenQuery request, CancellationToken cancellationToken)
            {
                var userToCheck = await _userRepository.GetByRefreshToken(request.RefreshToken);
                if (userToCheck == null)
                {
                    return new ErrorDataResult<User>(Messages.UserNotFound);
                }


				var claims = _userRepository.GetClaims(userToCheck.UserId);
				_cacheManager.Remove($"{CacheKeys.UserIdForClaim}={userToCheck.UserId}");
				_cacheManager.Add($"{CacheKeys.UserIdForClaim}={userToCheck.UserId}", claims.Select(x => x.Name));
				var accessToken = _tokenHelper.CreateToken<AccessToken>(userToCheck);
				userToCheck.RefreshToken = accessToken.RefreshToken;
				_userRepository.Update(userToCheck);
				await _userRepository.SaveChangesAsync();
				return new SuccessDataResult<AccessToken>(accessToken, Messages.SuccessfulLogin);
			}
		}
	}
}


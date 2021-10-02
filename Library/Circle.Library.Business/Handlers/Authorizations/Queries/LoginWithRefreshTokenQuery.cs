using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Circle.Core.CrossCuttingConcerns.Caching;
using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Authorizations.Queries
{
    public class LoginWithRefreshTokenQuery : IRequest<ResponseMessage<AccessToken>>
    {
        public string RefreshToken { get; set; }

        public class LoginWithRefreshTokenQueryHandler : IRequestHandler<LoginWithRefreshTokenQuery, ResponseMessage<AccessToken>>
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

            public async Task<ResponseMessage<AccessToken>> Handle(LoginWithRefreshTokenQuery request, CancellationToken cancellationToken)
            {
                var userToCheck = await _userRepository.GetByRefreshToken(request.RefreshToken);
                if (userToCheck == null)
                {
                    return ResponseMessage<AccessToken>.NoDataFound("Kullanýcý bulunamadý.");
                }


				var claims = _userRepository.GetClaims(userToCheck.Id);
				_cacheManager.Remove($"{CacheKeys.UserIdForClaim}={userToCheck.Id}");
				_cacheManager.Add($"{CacheKeys.UserIdForClaim}={userToCheck.Id}", claims.Select(x => x.Name));
				var accessToken = _tokenHelper.CreateToken<AccessToken>(userToCheck);
				userToCheck.RefreshToken = accessToken.RefreshToken;
				_userRepository.Update(userToCheck);
				await _userRepository.SaveChangesAsync();
                return ResponseMessage<AccessToken>.Success(accessToken);
			}
		}
	}
}


using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.Constants;
using Circle.Library.Business.Services.Authentication;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Caching;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Hashing;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Core.Aspects.Autofac.Validation;
using Circle.Library.Business.Handlers.Authorizations.ValidationRules;
using Circle.Library.Entities.ComplexTypes;

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

            public LoginUserQueryHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IMediator mediator, ICacheManager cacheManager)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _mediator = mediator;
                _cacheManager = cacheManager;
            }

            [ValidationAspect(typeof(LoginUserValidator), Priority = 1)]
            public async Task<ResponseMessage<AccessToken>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.Email == request.LoginModel.Email && u.Status);

                if (user == null)
                {
                    return ResponseMessage<AccessToken>.NoDataFound("Kullanıcı bulunamadı.");
                }

                if (!HashingHelper.VerifyPasswordHash(request.LoginModel.Password, user.Password))
                {
                    return ResponseMessage<AccessToken>.NoDataFound("Kullanıcı adı yada şifre hatalı.");
                }

                var claims = _userRepository.GetClaims(user.Id);

                var accessToken = _tokenHelper.CreateToken<DArchToken>(user);
                accessToken.Claims = claims.Select(x => x.Name).ToList();

                user.RefreshToken = accessToken.RefreshToken;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                _cacheManager.Add($"{CacheKeys.UserIdForClaim}={user.Id}", claims.Select(x => x.Name));

                return ResponseMessage<AccessToken>.Success(accessToken);
            }
        }
    }
}
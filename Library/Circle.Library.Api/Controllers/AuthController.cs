using System.Threading.Tasks;
using Circle.Library.Business.Handlers.Authorizations.Commands;
using Circle.Library.Business.Handlers.Authorizations.Queries;
using Circle.Library.Business.Handlers.Users.Commands;
using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Circle.Library.Entities.ComplexTypes;

namespace Circle.Library.Api.Controllers
{
    /// <summary>
    /// Make it Authorization operations
    /// </summary>
    //[Route("api/[controller]")]
    [Route("api/{culture:culture}/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IHttpContextAccessor _localizer;

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Dependency injection is provided by constructor injection.
        /// </summary>
        /// <param name="configuration"></param>
        public AuthController(IConfiguration configuration,
            IHttpContextAccessor localizer)
        {
            _configuration = configuration;
            _localizer = localizer;
        }

        /// <summary>
        /// Make it User Login operations
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<AccessToken>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            LoginUserQuery query = new LoginUserQuery()
            {
                LoginModel = loginModel
            };

            return CreateActionResultInstance(await Mediator.Send(query));
        }

        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<AccessToken>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> LoginWithRefreshToken([FromBody] LoginModel loginModel)
        {
            LoginWithRefreshTokenQuery query = new LoginWithRefreshTokenQuery()
            {
                RefreshToken = loginModel.RefreshToken
            };

            return CreateActionResultInstance(await Mediator.Send(query));
        }

        /// <summary>
        ///  Make it User Register operations
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand createUser)
        {
            return GetResponseOnlyResult(await Mediator.Send(createUser));
        }

        /// <summary>
        /// Make it Forgot Password operations
        /// </summary>
        /// <remarks>tckimlikno</remarks>
        /// <return></return>
        /// <response code="200"></response>
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IResult))]
        [HttpPut]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand forgotPassword)
        {
            return GetResponseOnlyResult(await Mediator.Send(forgotPassword));
        }

        /// <summary>
        /// Make it Change Password operation
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> ChangeUserPassword([FromBody] UserChangePasswordCommand command)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(command));
        }
    }
}
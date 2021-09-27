using Circle.Library.Business.Constants;
using Circle.Core.Entities.Concrete;
using FluentValidation;
using Circle.Library.Business.Services.Authentication.Model;

namespace Circle.Library.Business.Handlers.Authorizations.ValidationRules
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(m => m.Password).NotEmpty()
                .When((i) => i.Provider != AuthenticationProviderType.Person);
            RuleFor(m => m.ExternalUserId).NotEmpty().Must((instance, value) =>
                {
                    switch (instance.Provider)
                    {
                        case AuthenticationProviderType.Person:
                            return true;
                        case AuthenticationProviderType.Staff:
                            return true;
                        case AuthenticationProviderType.Agent:
                            break;
                        case AuthenticationProviderType.Unknown:
                            break;
                        default:
                            break;
                    }

                    return false;
                })
                .WithMessage(Messages.InvalidCode)
                .OverridePropertyName(Messages.CitizenNumber);
        }
    }
}
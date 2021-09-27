using Circle.Library.Business.Handlers.Authorizations.Commands;
using FluentValidation;

namespace Circle.Library.Business.Handlers.Authorizations.ValidationRules
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(p => p.Password).Password();
        }
    }
}
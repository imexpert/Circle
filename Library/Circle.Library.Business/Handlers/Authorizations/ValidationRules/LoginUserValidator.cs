using Circle.Library.Business.Constants;
using Circle.Core.Entities.Concrete;
using FluentValidation;
using Circle.Library.Business.Services.Authentication.Model;
using Circle.Library.Business.Handlers.Authorizations.Queries;

namespace Circle.Library.Business.Handlers.Authorizations.ValidationRules
{
    public class LoginUserValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserValidator()
        {
            RuleFor(m => m.LoginModel.Password).NotNull().WithMessage("Şifre boş olamaz");
            RuleFor(m => m.LoginModel.Email).NotNull().WithMessage("E-Mail adresi boş olamaz");
        }
    }
}
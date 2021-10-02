using Circle.Library.Business.Handlers.Languages.Commands;
using FluentValidation;

namespace Circle.Library.Business.Handlers.Languages.ValidationRules
{
    public class CreateLanguageValidator : AbstractValidator<CreateLanguageCommand>
    {
        public CreateLanguageValidator()
        {
            
        }
    }

    public class UpdateLanguageValidator : AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageValidator()
        {
            
        }
    }
}
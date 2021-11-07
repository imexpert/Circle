using Circle.Core.Utilities.IoC;
using System;
using Microsoft.Extensions.DependencyInjection;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;

namespace Circle.Frontends.Web.Infrastructure.Extensions
{
    public static class LanguageExtension
    {
        public static Guid TrLanguageId
        {
            get
            {
                ILanguageService _languageService = ServiceTool.ServiceProvider.GetService<ILanguageService>();
                ResponseMessage<Language> languageResponse = _languageService.GetLanguage("tr-TR").GetAwaiter().GetResult();
                if (languageResponse.IsSuccess)
                {
                    return languageResponse.Data.Id;
                }

                return Guid.Empty;
            }
        }

        public static Guid UsLanguageId
        {
            get
            {
                ILanguageService _languageService = ServiceTool.ServiceProvider.GetService<ILanguageService>();
                ResponseMessage<Language> languageResponse = _languageService.GetLanguage("tr-TR").GetAwaiter().GetResult();
                if (languageResponse.IsSuccess)
                {
                    return languageResponse.Data.Id;
                }

                return Guid.Empty;
            }
        }
    }
}

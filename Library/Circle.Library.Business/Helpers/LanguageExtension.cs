using Circle.Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Circle.Library.DataAccess.Abstract;
using Circle.Core.Entities.Concrete;

namespace Circle.Library.Business.Helpers
{
    public static class LanguageExtension
    {
        public static Guid LanguageId 
        {
            get
            {
                var context = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                ILanguageRepository  _languageRepository = ServiceTool.ServiceProvider.GetService<ILanguageRepository>();
                string path = context.HttpContext.Request.Path.Value.Split('/')[2];
                Language language = _languageRepository.Get(s => s.Code == path);
                return language.Id;
            }
        }

        public static Guid TrLanguageId
        {
            get
            {
                ILanguageRepository _languageRepository = ServiceTool.ServiceProvider.GetService<ILanguageRepository>();
                Language language = _languageRepository.Get(s => s.Code == "tr-TR");
                return language.Id;
            }
        }

        public static Guid UsLanguageId
        {
            get
            {
                ILanguageRepository _languageRepository = ServiceTool.ServiceProvider.GetService<ILanguageRepository>();
                Language language = _languageRepository.Get(s => s.Code == "en-US");
                return language.Id;
            }
        }
    }
}

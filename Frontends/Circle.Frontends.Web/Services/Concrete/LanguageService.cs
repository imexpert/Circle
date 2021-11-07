using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Frontends.Web.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Concrete
{
    public class LanguageService : ILanguageService
    {
        HttpClient _client;

        public LanguageService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ResponseMessage<Language>> GetLanguage(string code)
        {
            ResponseMessage<Language> result = new ResponseMessage<Language>();

            HttpResponseMessage response = await _client.GetAsync("Departments/GetByCode/" + code);
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<Language>>(data);
        }
    }
}

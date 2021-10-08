using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Concrete
{
    public class AuthService : IAuthService
    {
        HttpClient _client;

        public AuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ResponseMessage<AccessToken>> LoginAsync(LoginModel loginModel)
        {
            string json = JsonConvert.SerializeObject(loginModel,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _client.PostAsync("Auth/Login", content);
                // Get string data
                string data = await response.Content.ReadAsStringAsync();
                // Deserialize the data
                return JsonConvert.DeserializeObject<ResponseMessage<AccessToken>>(data);
            }
        }
    }
}

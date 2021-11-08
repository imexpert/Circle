using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Concrete
{
    public class UserService : IUserService
    {
        HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseMessage<List<User>>> GetList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Users/GetList");
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<User>>>(data);
        }

        public async Task<ResponseMessage<CreateUserModel>> AddAsync(CreateUserModel model)
        {
            string json = JsonConvert.SerializeObject(model,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _httpClient.PostAsync("Users/Add", content);

                // Get string data
                string data = await response.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<ResponseMessage<CreateUserModel>>(data);

                // Deserialize the data
                return user;
            }
        }
    }
}

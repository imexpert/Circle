using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Frontends.Web.Services.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Concrete
{
    public class GroupService : IGroupService
    {
        HttpClient _httpClient;

        public GroupService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseMessage<List<Group>>> GetList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("tr-TR/Groups/GetAll");
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<Group>>>(data);
        }
    }
}

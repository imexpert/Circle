using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            HttpResponseMessage response = await _httpClient.GetAsync("Groups/GetAll");
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<Group>>>(data);
        }

        public async Task<ResponseMessage<Group>> AddAsync(Group groupModel)
        {
            string json = JsonConvert.SerializeObject(groupModel,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _httpClient.PostAsync("Groups/Add", content);

                // Get string data
                string data = await response.Content.ReadAsStringAsync();

                var group = JsonConvert.DeserializeObject<ResponseMessage<Group>>(data);

                if (group.IsSuccess)
                {
                    await AddAsync(group.Data);
                }

                // Deserialize the data
                return group;
            }
        }

        public async Task<ResponseMessage<Group>> UpdateAsync(Group groupModel)
        {
            string json = JsonConvert.SerializeObject(groupModel,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _httpClient.PostAsync("Groups/Update", content);

                // Get string data
                string data = await response.Content.ReadAsStringAsync();

                var group = JsonConvert.DeserializeObject<ResponseMessage<Group>>(data);

                // Deserialize the data
                return group;
            }
        }

        public async Task<ResponseMessage<List<Group>>> GetWithIdAsync(Guid groupId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Groups/GetWithId/" + groupId);
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<Group>>>(data);
        }

        public async Task<ResponseMessage<List<Group>>> DeleteAsync(Guid groupId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Groups/Delete/" + groupId);
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<Group>>>(data);
        }



        public async Task<ResponseMessage<List<GroupModel>>> GetWithClaimsAsync(Guid groupId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Groups/GetWithClaims/" + groupId);
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<GroupModel>>>(data);
        }
    }
}

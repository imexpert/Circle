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
    public class GroupClaimService : IGroupClaimService
    {
        HttpClient _httpClient;

        public GroupClaimService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseMessage<List<GroupClaim>>> GetList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("GroupClaims/GetAll");
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<GroupClaim>>>(data);
        }

        public async Task<ResponseMessage<GroupClaim>> AddAsync(GroupClaim groupClaimModel)
        {
            string json = JsonConvert.SerializeObject(groupClaimModel,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _httpClient.PostAsync("GroupClaims/Add", content);

                // Get string data
                string data = await response.Content.ReadAsStringAsync();

                var groupClaim = JsonConvert.DeserializeObject<ResponseMessage<GroupClaim>>(data);

                // Deserialize the data
                return groupClaim;
            }
        }

        public async Task<ResponseMessage<GroupClaim>> UpdateAsync(GroupClaim groupClaimModel)
        {
            string json = JsonConvert.SerializeObject(groupClaimModel,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _httpClient.PostAsync("GroupClaims/Update", content);

                // Get string data
                string data = await response.Content.ReadAsStringAsync();

                var groupClaim = JsonConvert.DeserializeObject<ResponseMessage<GroupClaim>>(data);

                // Deserialize the data
                return groupClaim;
            }
        }

        public async Task<ResponseMessage<List<GroupClaim>>> GetWithIdAsync(Guid groupClaimId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("GroupClaims/GetWithId/" + groupClaimId);
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<GroupClaim>>>(data);
        }

        public async Task<ResponseMessage<List<GroupClaim>>> DeleteAsync(Guid groupClaimId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("OperationClaims/Delete/" + groupClaimId);
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<GroupClaim>>>(data);
        }
    }
}

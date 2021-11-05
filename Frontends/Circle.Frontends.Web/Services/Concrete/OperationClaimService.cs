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
    public class OperationClaimService : IOperationClaimService
    {
        HttpClient _httpClient;

        public OperationClaimService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseMessage<List<OperationClaim>>> GetList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("OperationClaims/GetAll");
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<OperationClaim>>>(data);
        }

        public async Task<ResponseMessage<OperationClaim>> AddAsync(OperationClaim operationClaimModel)
        {
            string json = JsonConvert.SerializeObject(operationClaimModel,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _httpClient.PostAsync("OperationClaims/Add", content);

                // Get string data
                string data = await response.Content.ReadAsStringAsync();

                var operationClaim = JsonConvert.DeserializeObject<ResponseMessage<OperationClaim>>(data);

                // Deserialize the data
                return operationClaim;
            }
        }

        public async Task<ResponseMessage<OperationClaim>> UpdateAsync(OperationClaim operationClaimModel)
        {
            string json = JsonConvert.SerializeObject(operationClaimModel,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _httpClient.PostAsync("OperationClaims/Update", content);

                // Get string data
                string data = await response.Content.ReadAsStringAsync();

                var operationClaim = JsonConvert.DeserializeObject<ResponseMessage<OperationClaim>>(data);

                // Deserialize the data
                return operationClaim;
            }
        }

        public async Task<ResponseMessage<List<OperationClaim>>> GetWithIdAsync(Guid operationClaimId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Groups/GetWithId/" + operationClaimId);
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<OperationClaim>>>(data);
        }

        public async Task<ResponseMessage<List<OperationClaim>>> DeleteAsync(Guid operationClaimId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("OperationClaims/Delete/" + operationClaimId);
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<OperationClaim>>>(data);
        }
    }
}

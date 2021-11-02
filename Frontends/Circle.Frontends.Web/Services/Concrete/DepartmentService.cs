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
    public class DepartmentService : IDepartmentService
    {
        HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseMessage<CreateDepartmentModel>> AddAsync(CreateDepartmentModel model)
        {
            string json = JsonConvert.SerializeObject(model,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _httpClient.PostAsync("Departments/Add", content);

                // Get string data
                string data = await response.Content.ReadAsStringAsync();

                var department = JsonConvert.DeserializeObject<ResponseMessage<CreateDepartmentModel>>(data);

                // Deserialize the data
                return department;
            }
        }

        public async Task<ResponseMessage<List<Department>>> GetList()
        {
            ResponseMessage<List<Department>> result = new ResponseMessage<List<Department>>();

            HttpResponseMessage response = await _httpClient.GetAsync("Departments/GetAll");
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<Department>>>(data);
        }
    }
}

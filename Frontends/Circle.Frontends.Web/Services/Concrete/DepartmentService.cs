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
    public class DepartmentService : IDepartmentService
    {
        HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseMessage<List<Department>>> GetList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Departments/GetList");
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<Department>>>(data);
        }
    }
}

using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Concrete
{
    public class CategoryAttributeService : ICategoryAttributeService
    {
        HttpClient _client;

        public CategoryAttributeService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ResponseMessage<List<CategoryAttribute>>> GetAllAttributes(Guid categoryId)
        {
            HttpResponseMessage response = await _client.GetAsync("CategoryAttributes/GetAllAttributes?categoryId=" + categoryId);
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<List<CategoryAttribute>>>(data);
        }
    }
}

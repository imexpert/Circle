using Circle.Core.Utilities.Results;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Entities.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Concrete
{
    public class ProductService : IProductService
    {
        HttpClient _client;

        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ResponseMessage<Product>> AddAsync(Product model)
        {
            string json = JsonConvert.SerializeObject(model,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _client.PostAsync("Products/Add", content);

                // Get string data
                string data = await response.Content.ReadAsStringAsync();

                var product = JsonConvert.DeserializeObject<ResponseMessage<Product>>(data);

                // Deserialize the data
                return product;
            }
        }

        public async Task<ResponseMessage<ProductModel>> GetAsync(Guid productId)
        {
            HttpResponseMessage response = await _client.GetAsync("Products/GetWithId?productId=" + productId);
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage<ProductModel>>(data);
        }
    }
}

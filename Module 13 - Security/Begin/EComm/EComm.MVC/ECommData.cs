using EComm.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EComm.MVC
{
    public class ECommData
    {
        private const string BaseAddress = "http://localhost:59394/";
        private const string Route = "/api/Product/";
        private const string ContentType = "application/json";

        private HttpClient client =
            new HttpClient(new HttpClientHandler { UseDefaultCredentials = true });
        public ECommData()
        {
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(ContentType));
        }

        public IEnumerable<Product> GetProducts()
        {
            HttpResponseMessage response = client.GetAsync(Route).Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
            return JsonConvert.DeserializeObject<List<Product>>(
                response.Content.ReadAsStringAsync().Result);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await client.GetAsync(Route);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }

            var stringResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Product>>(stringResult);
        }

        public Product GetProduct(int id)
        {
            var response = client.GetAsync(Route + id).Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
            return JsonConvert.DeserializeObject<Product>(
                response.Content.ReadAsStringAsync().Result);
        }

    }
}

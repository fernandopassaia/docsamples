using System.Net.Http;
using System.Threading.Tasks;

namespace Natific.Ui.ApiInterface
{
    public class ProductApi : ApiBase, IProductApi
    {
        private readonly string _ProductsUrl = "products";
        private readonly string _ProductsPUrl = "products/{0}";

        public async Task<HttpResponseMessage> GetProductAsync()
        {
            using (var client = GetWebApiUrl())
            {
                return await client.GetAsync(_ProductsUrl);
            }
        }

        public async Task<HttpResponseMessage> GetStatistics()
        {
            using (var client = GetWebApiUrl())
            {
                return await client.GetAsync(_ProductsUrl + "/statistics");
            }
        }

        public async Task<HttpResponseMessage> GetProductById(int id)
        {
            using (var client = GetWebApiUrl())
            {
                return await client.GetAsync(string.Format(_ProductsPUrl, id));
            }
        }

        public async Task<HttpResponseMessage> PostProduct<T>(T value)
        {
            //var client = GetWebApiUrl();
            //var serializer = new JavaScriptSerializer();
            //var json = serializer.Serialize(value);
            //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            //return await client.PostAsync(_ProductsUrl, stringContent);

            using (var client = GetWebApiUrl()) //i can do it this way, or other (commented)
            {
                return await client.PostAsJsonAsync(string.Format(_ProductsUrl), value);
            }
        }

        public async Task<HttpResponseMessage> PutProduct<T>(T value)
        {            
            using (var client = GetWebApiUrl())
            {
                return await client.PutAsJsonAsync(string.Format(_ProductsUrl), value);
            }
        }

        public async Task<HttpResponseMessage> DeleteProduct(int id)
        {            
            using (var client = GetWebApiUrl())
            {
                return await client.DeleteAsync(string.Format(_ProductsPUrl, id));
            }
        }
    }
}
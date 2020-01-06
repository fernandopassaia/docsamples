using System.Net.Http;
using System.Threading.Tasks;

namespace Bim.WebUI.InterfaceApi
{
    public class ProductClient : ApiBase, IProductClient
    {
        private readonly string _getManufacturerProductsUrl = "manufacturers/{0}/products";
        private readonly string _getManufacturerProductUrl = "manufacturers/{0}/products/{1}";
        private readonly string _getManufacturerProductImageUrl = "manufacturers/{0}/products/{1}/image";

        #region Gets
        public async Task<HttpResponseMessage> GetManufacturerProducts(int manufacturerId)
        {
            using (var client = GetWebApiClient())
            {
                return await client.GetAsync(string.Format(_getManufacturerProductsUrl, manufacturerId));
            }
        }

        public async Task<HttpResponseMessage> GetManufacturerProduct(int manufacturerId, int id)
        {
            using (var client = GetWebApiClient())
            {
                return await client.GetAsync(string.Format(_getManufacturerProductUrl, manufacturerId, id));
            }
        }

        public async Task<HttpResponseMessage> GetManufacturerProductImage(int manufacturerId, int id)
        {
            using (var client = GetWebApiClient())
            {
                return await client.GetAsync(string.Format(_getManufacturerProductImageUrl, manufacturerId, id));
            }
        }
        #endregion

        #region Post
        public async Task<HttpResponseMessage> PostManufacturerProduct<T>(int manufacturerId, T value)
        {
            using (var client = GetWebApiClient())
            {
                return await client.PostAsJsonAsync(string.Format(_getManufacturerProductsUrl, manufacturerId), value);
            }
        }

        public async Task<HttpResponseMessage> PostManufacturerProductImage(int manufacturerId, int id, System.Drawing.Image image)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                var bytes = stream.ToArray();
                var base64 = System.Convert.ToBase64String(bytes);

                using (var client = GetWebApiClient())
                {
                    var stringContent = new StringContent(base64, System.Text.Encoding.UTF8, "application/json");
                    return await client.PostAsync(string.Format(_getManufacturerProductImageUrl, manufacturerId, id), stringContent);
                }
            }
        }
        #endregion

        #region Put/Delete
        public async Task<HttpResponseMessage> PutManufacturerProduct<T>(int manufacturerId, int id, T value)
        {
            using (var client = GetWebApiClient())
            {
                return await client.PutAsJsonAsync(string.Format(_getManufacturerProductUrl, manufacturerId, id), value);
            }
        }

        public async Task<HttpResponseMessage> DeleteManufacturerProduct(int manufacturerId, int id)
        {
            using (var client = GetWebApiClient())
            {
                return await client.DeleteAsync(string.Format(_getManufacturerProductUrl, manufacturerId, id));
            }
        }
        #endregion
    }
}
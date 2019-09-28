using System.Net.Http;
using System.Threading.Tasks;

namespace Bim.WebUI.InterfaceApi
{
    public class ManufacturerClient : ApiBase, IManufacturerClient
    {
        private readonly string _getManufacturersUrl = "manufacturers";
        private readonly string _getManufacturerUrl = "manufacturers/{0}";
        private readonly string _getManufacturerImageUrl = "manufacturers/{0}/image";

        #region Get
        public async Task<HttpResponseMessage> GetManufacturers()
        {
            using (var client = GetWebApiClient())
            {
                return await client.GetAsync(_getManufacturersUrl);
            }
        }

        public async Task<HttpResponseMessage> GetManufacturer(int id)
        {
            using (var client = GetWebApiClient())
            {
                return await client.GetAsync(string.Format(_getManufacturerUrl, id));
            }
        }

        public async Task<HttpResponseMessage> GetManufacturerImage(int id)
        {
            using (var client = GetWebApiClient())
            {
                return await client.GetAsync(string.Format(_getManufacturerImageUrl, id));
            }
        }
        #endregion

        #region Post
        public async Task<HttpResponseMessage> PostManufacturer<T>(T value)
        {
            using (var client = GetWebApiClient())
            {
                return await client.PostAsJsonAsync(_getManufacturersUrl, value);
            }
        }

        public async Task<HttpResponseMessage> PostManufacturerImage(int id, System.Drawing.Image image)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                var bytes = stream.ToArray();
                var base64 = System.Convert.ToBase64String(bytes);

                using (var client = GetWebApiClient())
                {
                    var stringContent = new StringContent(base64, System.Text.Encoding.UTF8, "application/json");
                    return await client.PostAsync(string.Format(_getManufacturerImageUrl, id), stringContent);
                }
            }
        }
        #endregion

        #region Put/Delete
        public async Task<HttpResponseMessage> PutManufacturer<T>(int id, T value)
        {
            using (var client = GetWebApiClient())
            {
                return await client.PutAsJsonAsync(string.Format(_getManufacturerUrl, id), value);
            }
        }

        public async Task<HttpResponseMessage> DeleteManufacturer(int id)
        {
            using (var client = GetWebApiClient())
            {
                return await client.DeleteAsync(string.Format(_getManufacturerUrl, id));
            }
        }
        #endregion
    }
}
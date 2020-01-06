using System.Net.Http;
using System.Threading.Tasks;

namespace BimManufact.Web.Clients
{
    public interface IProductClient
    {
        Task<HttpResponseMessage> DeleteManufacturerProduct(int manufacturerId, int productId);
        Task<HttpResponseMessage> GetManufacturerProduct(int manufacturerId, int productId);
        Task<HttpResponseMessage> GetManufacturerProductImage(int manufacturerId, int productId);
        Task<HttpResponseMessage> GetManufacturerProducts(int manufacturerId);
        Task<HttpResponseMessage> PostManufacturerProduct<T>(int manufacturerId, T value);
        Task<HttpResponseMessage> PostManufacturerProductImage(int manufacturerId, int productId, System.Drawing.Image image);
        Task<HttpResponseMessage> PutManufacturerProduct<T>(int manufacturerId, int productId, T value);
    }
}
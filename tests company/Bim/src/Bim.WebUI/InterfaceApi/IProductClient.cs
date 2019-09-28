using System.Net.Http;
using System.Threading.Tasks;

namespace Bim.WebUI.InterfaceApi
{
    public interface IProductClient
    {
        Task<HttpResponseMessage> GetManufacturerProducts(int manufacturerId);
        Task<HttpResponseMessage> GetManufacturerProduct(int manufacturerId, int id);
        Task<HttpResponseMessage> GetManufacturerProductImage(int manufacturerId, int id);
        Task<HttpResponseMessage> PostManufacturerProduct<T>(int manufacturerId, T value);
        Task<HttpResponseMessage> PostManufacturerProductImage(int manufacturerId, int id, System.Drawing.Image image);
        Task<HttpResponseMessage> PutManufacturerProduct<T>(int manufacturerId, int id, T value);
        Task<HttpResponseMessage> DeleteManufacturerProduct(int manufacturerId, int id);
    }
}
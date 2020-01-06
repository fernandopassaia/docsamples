using System.Net.Http;
using System.Threading.Tasks;

namespace Bim.WebUI.InterfaceApi
{
    public interface IManufacturerClient
    {
        Task<HttpResponseMessage> GetManufacturers();
        Task<HttpResponseMessage> GetManufacturer(int id);
        Task<HttpResponseMessage> GetManufacturerImage(int id);        
        Task<HttpResponseMessage> PostManufacturer<T>(T value);
        Task<HttpResponseMessage> PostManufacturerImage(int id, System.Drawing.Image image);
        Task<HttpResponseMessage> PutManufacturer<T>(int id, T value);
        Task<HttpResponseMessage> DeleteManufacturer(int id);
    }
}
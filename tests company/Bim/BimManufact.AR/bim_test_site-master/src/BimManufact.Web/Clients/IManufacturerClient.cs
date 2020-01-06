using System.Net.Http;
using System.Threading.Tasks;

namespace BimManufact.Web.Clients
{
    public interface IManufacturerClient
    {
        Task<HttpResponseMessage> DeleteManufacturer(int id);
        Task<HttpResponseMessage> GetManufacturer(int id);
        Task<HttpResponseMessage> GetManufacturerLogo(int id);
        Task<HttpResponseMessage> GetManufacturers();
        Task<HttpResponseMessage> PostManufacturer<T>(T value);
        Task<HttpResponseMessage> PostManufacturerLogo(int id, System.Drawing.Image image);
        Task<HttpResponseMessage> PutManufacturer<T>(int id, T value);
    }
}
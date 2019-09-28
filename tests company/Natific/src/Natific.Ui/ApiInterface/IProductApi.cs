using System.Net.Http;
using System.Threading.Tasks;

namespace Natific.Ui.ApiInterface
{
    public interface IProductApi
    {
        Task<HttpResponseMessage> GetProductAsync();
        Task<HttpResponseMessage> GetStatistics();
        Task<HttpResponseMessage> GetProductById(int id);
        Task<HttpResponseMessage> PostProduct<T>(T value);
        Task<HttpResponseMessage> PutProduct<T>(T value);
        Task<HttpResponseMessage> DeleteProduct(int id);
    }
}

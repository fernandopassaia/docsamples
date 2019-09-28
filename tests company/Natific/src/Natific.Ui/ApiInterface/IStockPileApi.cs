using System.Net.Http;
using System.Threading.Tasks;

namespace Natific.Ui.ApiInterface
{
    public interface IStockPileApi
    {
        Task<HttpResponseMessage> GetStockPiles(int id);
        Task<HttpResponseMessage> PostStockPile<T>(T value);
    }
}
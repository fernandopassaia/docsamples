using System.Net.Http;
using System.Threading.Tasks;

namespace Natific.Ui.ApiInterface
{
    public class StockPileApi : ApiBase, IStockPileApi
    {
        private readonly string _StockPilesUrl = "stockpiles";
        private readonly string _StockPilesPUrl = "stockpiles/{0}";

        public async Task<HttpResponseMessage> GetStockPiles(int id)
        {
            using (var client = GetWebApiUrl())
            {
                return await client.GetAsync(string.Format(_StockPilesPUrl, id));
            }
        }

        public async Task<HttpResponseMessage> PostStockPile<T>(T value)
        {
            //var client = GetWebApiUrl();
            //var serializer = new JavaScriptSerializer();
            //var json = serializer.Serialize(value);
            //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            //return await client.PostAsync(_ProductsUrl, stringContent);

            using (var client = GetWebApiUrl()) //i can do it this way, or other (commented)
            {
                return await client.PostAsJsonAsync(string.Format(_StockPilesUrl), value);
            }
        }
    }
}
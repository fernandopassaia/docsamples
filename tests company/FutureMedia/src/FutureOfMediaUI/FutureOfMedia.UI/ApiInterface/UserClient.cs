using FutureOfMedia.UI.Commands.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutureOfMedia.UI.ApiInterface
{
    public partial class ApiClient
    {
        public async Task<IEnumerable<GetUserResult>> GetUsers()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                ""));
            return await GetAsync<IEnumerable<GetUserResult>>(requestUrl);            
        }

        //public async Task<Message<UsersModel>> SaveUser(UsersModel model)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        "User/SaveUser"));
        //    return await PostAsync<UsersModel>(requestUrl, model);
        //}
    }
}

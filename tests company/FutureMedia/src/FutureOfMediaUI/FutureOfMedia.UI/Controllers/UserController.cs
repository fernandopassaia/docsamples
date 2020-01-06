using FutureOfMedia.UI.ApiInterface;
using FutureOfMedia.UI.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FutureOfMedia.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;

        public UserController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
        }

        public async Task<IActionResult> Index()
        {
            var data = await ApiClientFactory.Instance.GetUsers();
            //var response = await SaveUser();
            return View(data);
        }
    }
}
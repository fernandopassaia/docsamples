using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using BimManufact.Web.Clients;
using BimManufact.Web.Models;

namespace BimManufact.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IManufacturerClient _client;
        private readonly string _genericErrorMessage = "Server error, please try again.";
        private readonly string[] _validImageExtensions = new[] { ".JPG", ".GIF", ".PNG" };

        public HomeController(IManufacturerClient client)
        {
            _client = client;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<ManufacturerViewModel> members = null;

            var response = await _client.GetManufacturers();

            if (response.IsSuccessStatusCode)
            {
                members = await response.Content.ReadAsAsync<IList<ManufacturerViewModel>>();
            }
            else
            {
                members = Enumerable.Empty<ManufacturerViewModel>();
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(members.OrderBy(_ => _.Name));
        }

        public ActionResult Create()
        {
            return View(new ManufacturerRequestViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(ManufacturerRequestViewModel request)
        {
            var result = await _client.PostManufacturer(request);
            var newViewModel = await result.Content.ReadAsAsync<ManufacturerRequestViewModel>();

            if (result.IsSuccessStatusCode)
            {
                if (Request.Files.Count > 0
                    && _validImageExtensions.Contains(System.IO.Path.GetExtension(Request.Files[0].FileName), System.StringComparer.OrdinalIgnoreCase))
                {
                    var image = System.Drawing.Image.FromStream(Request.Files[0].InputStream);
                    var logoResult = await _client.PostManufacturerLogo(newViewModel.ManufacturerId, image);
                }

                TempData["SuccessAlert"] = $"The '{ request.Name }' manufacturer was successfully created!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(request);
        }

        public async Task<ActionResult> Delete(int id = 0)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Mandatory argument was not specified");
            }

            var result = await _client.DeleteManufacturer(id);

            if (result.IsSuccessStatusCode)
            {
                TempData["SuccessAlert"] = $"The manufacturer was successfully deleted!";
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Update(int id = -1)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Mandatory argument was not specified");
            }

            ManufacturerRequestViewModel request = null;

            var response = await _client.GetManufacturer(id);

            if (response.IsSuccessStatusCode)
            {
                request = await response.Content.ReadAsAsync<ManufacturerRequestViewModel>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(request);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ManufacturerRequestViewModel request)
        {
            var result = await _client.PutManufacturer(request.ManufacturerId, request);

            if (result.IsSuccessStatusCode)
            {
                TempData["SuccessAlert"] = "The manufacturer was successfully updated!";
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(request);
        }

        public async Task<ActionResult> GetManufacturerLogo(int id)
        {
            var response = await _client.GetManufacturerLogo(id);
            var result = await response.Content.ReadAsByteArrayAsync();

            return File(result, "image/png");
        }
    }
}
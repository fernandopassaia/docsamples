using Bim.WebUI.InterfaceApi;
using Bim.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bim.WebUI.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerClient _client;
        private readonly string _genericErrorMessage = "Server error, check if API is running.";
        private readonly string[] _validImageExtensions = new[] { ".PNG" }; //accept png because the transparent background

        public ManufacturerController(IManufacturerClient client)
        {
            _client = client;
        }

        #region Index/Get
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

        public async Task<ActionResult> GetManufacturerImage(int id)
        {
            var response = await _client.GetManufacturerImage(id);
            var result = await response.Content.ReadAsByteArrayAsync();

            return File(result, "image/png");
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View(new ManufacturerViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(ManufacturerViewModel request)
        {
            var result = await _client.PostManufacturer(request);
            var newViewModel = await result.Content.ReadAsAsync<ManufacturerViewModel>();

            if (result.IsSuccessStatusCode)
            {
                if (Request.Files.Count > 0
                    && _validImageExtensions.Contains(System.IO.Path.GetExtension(Request.Files[0].FileName), System.StringComparer.OrdinalIgnoreCase))
                {
                    var image = System.Drawing.Image.FromStream(Request.Files[0].InputStream);

                    //here i resize the image to 256x256 (i choose to do it here on UI, because this way i already transfer a small-image to API using less network)
                    image = image.GetThumbnailImage(256, 256, null, IntPtr.Zero);

                    //if you want to check for the resize work, uncoment the code below and check the file in ResizedImages folder on root)
                    //image.Save(Server.MapPath("~\\ResizedImages\\resized.png"), System.Drawing.Imaging.ImageFormat.Png);

                    var logoResult = await _client.PostManufacturerImage(newViewModel.id, image);
                }

                TempData["ManufacturerSuccessAlert"] = $"The '{ request.Name }' manufacturer was created.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(request);
        }
        #endregion
        
        #region Update
        public async Task<ActionResult> Update(int id = -1)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Should inform Manufacturer ID");
            }

            ManufacturerViewModel request = null;

            var response = await _client.GetManufacturer(id);

            if (response.IsSuccessStatusCode)
            {
                request = await response.Content.ReadAsAsync<ManufacturerViewModel>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(request);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ManufacturerViewModel request)
        {
            var result = await _client.PutManufacturer(request.id, request);

            if (result.IsSuccessStatusCode)
            {
                TempData["ManufacturerSuccessAlert"] = "The manufacturer was updated.";
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }
            return RedirectToAction(nameof(Index));            
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(int id = 0)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Should inform Manufacturer ID");
            }

            var result = await _client.DeleteManufacturer(id);

            if (result.IsSuccessStatusCode)
            {
                TempData["ManufacturerSuccessAlert"] = $"The manufacturer was successfully removed!";
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
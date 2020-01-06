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
    public class ManufacturerProductController : Controller
    {
        private readonly IProductClient _proClient;
        private readonly IManufacturerClient _manClient;
        private readonly string _genericErrorMessage = "Server error, check if API is running.";
        private readonly string[] _validImageExtensions = new[] { ".PNG" }; //accept png because the transparent background

        public ManufacturerProductController(IProductClient productClient, IManufacturerClient manufacturerClient)
        {
            _proClient = productClient;
            _manClient = manufacturerClient;
        }

        #region Index/Get
        public async Task<ActionResult> Index(int manufacturerId = 0)
        {
            if (manufacturerId <= 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Should inform Manufacturer ID");
            }

            var result = new ManufacturerProductViewModel();

            var manufacturerTask = _manClient.GetManufacturer(manufacturerId);
            var manufacturerProductsTask = _proClient.GetManufacturerProducts(manufacturerId);
            await Task.WhenAll(new[] { manufacturerTask, manufacturerProductsTask });

            if (manufacturerTask.Result.IsSuccessStatusCode)
            {
                result.Manufacturer = await manufacturerTask.Result.Content.ReadAsAsync<ManufacturerViewModel>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            if (manufacturerProductsTask.Result.IsSuccessStatusCode)
            {
                result.Products = (await manufacturerProductsTask.Result.Content.ReadAsAsync<IEnumerable<ProductViewModel>>()).OrderBy(_ => _.Name);
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(result);
        }

        public async Task<ActionResult> GetManufacturerProductImage(int manufacturerId, int id)
        {
            var response = await _proClient.GetManufacturerProductImage(manufacturerId, id);
            var result = await response.Content.ReadAsByteArrayAsync();

            return File(result, "image/png");
        }
        #endregion

        #region Create
        public ActionResult Create(int manufacturerId)
        {
            return View(new ProductViewModel { ManufacturerId = manufacturerId });
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel request)
        {
            var result = await _proClient.PostManufacturerProduct(request.ManufacturerId, request);
            var newViewModel = await result.Content.ReadAsAsync<ProductViewModel>();

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

                    await _proClient.PostManufacturerProductImage(newViewModel.ManufacturerId, newViewModel.id, image);
                }

                TempData["ProductSuccessAlert"] = $"The '{ request.Name }' product was created.";
                return RedirectToAction(nameof(Index), new { request.ManufacturerId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(request);
        }
        #endregion

        #region Update
        public async Task<ActionResult> Update(int manufacturerId, int id = -1)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Should inform Manufacturer ID");
            }

            ProductViewModel request = null;

            var response = await _proClient.GetManufacturerProduct(manufacturerId, id);

            if (response.IsSuccessStatusCode)
            {
                request = await response.Content.ReadAsAsync<ProductViewModel>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(request);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductViewModel request)
        {
            var result = await _proClient.PutManufacturerProduct(request.ManufacturerId, request.id, request);

            if (result.IsSuccessStatusCode)
            {
                TempData["ProductSuccessAlert"] = "The product was successfully updated!";
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }
            return RedirectToAction(nameof(Index), new { request.ManufacturerId });            
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(int manufacturerId, int id)
        {
            var result = await _proClient.DeleteManufacturerProduct(manufacturerId, id);

            if (result.IsSuccessStatusCode)
            {
                TempData["ProductSuccessAlert"] = $"The product was successfully removed!";
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return RedirectToAction(nameof(Index), new { manufacturerId });
        }
        #endregion
    }
}
using BimManufact.Web.Clients;
using BimManufact.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BimManufact.Web.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerClient _manufacturerClient;
        private readonly IProductClient _productClient;
        private readonly string _genericErrorMessage = "Server error, please try again.";
        private readonly string[] _validImageExtensions = new[] { ".JPG", ".GIF", ".PNG" };

        public ManufacturerController(IProductClient productClient, IManufacturerClient manufacturerClient)
        {
            _manufacturerClient = manufacturerClient;
            _productClient = productClient;
        }

        public async Task<ActionResult> Index(int manufacturerId = 0)
        {
            if (manufacturerId <= 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Mandatory argument was not specified");
            }

            var result = new ManufacturerProductsViewModel();

            var manufacturerTask = _manufacturerClient.GetManufacturer(manufacturerId);
            var manufacturerProductsTask = _productClient.GetManufacturerProducts(manufacturerId);
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

        public ActionResult Create(int manufacturerId)
        {
            return View(new ProductViewModel { ManufacturerId = manufacturerId });
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel request)
        {
            var result = await _productClient.PostManufacturerProduct(request.ManufacturerId, request);
            var newViewModel = await result.Content.ReadAsAsync<ProductViewModel>();

            if (result.IsSuccessStatusCode)
            {
                if (Request.Files.Count > 0
                    && _validImageExtensions.Contains(System.IO.Path.GetExtension(Request.Files[0].FileName), System.StringComparer.OrdinalIgnoreCase))
                {
                    var image = System.Drawing.Image.FromStream(Request.Files[0].InputStream);
                    await _productClient.PostManufacturerProductImage(newViewModel.ManufacturerId, newViewModel.ProductId, image);
                }

                TempData["ProductSuccessAlert"] = $"The '{ request.Name }' product was successfully created!";
                return RedirectToAction(nameof(Index), new { request.ManufacturerId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(request);
        }

        public async Task<ActionResult> Delete(int manufacturerId, int productId)
        {
            var result = await _productClient.DeleteManufacturerProduct(manufacturerId, productId);

            if (result.IsSuccessStatusCode)
            {
                TempData["ProductSuccessAlert"] = $"The product was successfully deleted!";
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return RedirectToAction(nameof(Index), new { manufacturerId });
        }

        public async Task<ActionResult> Update(int manufacturerId, int id = -1)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Mandatory argument was not specified");
            }

            ProductViewModel request = null;

            var response = await _productClient.GetManufacturerProduct(manufacturerId, id);

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
            var result = await _productClient.PutManufacturerProduct(request.ManufacturerId, request.ProductId, request);

            if (result.IsSuccessStatusCode)
            {
                TempData["ProductSuccessAlert"] = "The product was successfully updated!";
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(request);
        }

        public async Task<ActionResult> GetManufacturerProductImage(int manufacturerId, int id)
        {
            var response = await _productClient.GetManufacturerProductImage(manufacturerId, id);
            var result = await response.Content.ReadAsByteArrayAsync();

            return File(result, "image/png");
        }
    }
}
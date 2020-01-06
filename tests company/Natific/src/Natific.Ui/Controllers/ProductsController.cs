using Natific.Ui.ApiInterface;
using Natific.Ui.Models;
using Natific.Ui.Models.Inputs;
using Natific.Ui.Models.Results;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Natific.Ui.Controllers
{
    public class ProductsController : Controller
    {
        #region Constructor and Index/List
        private readonly IProductApi _client;
        private readonly string _genericErrorMessage = "Server error, check if API is running.";

        public ProductsController(IProductApi client)
        {
            _client = client;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<GetProductResult> members = null;

            var response = await _client.GetProductAsync();

            if (response.IsSuccessStatusCode)
            {
                members = await response.Content.ReadAsAsync<IList<GetProductResult>>();                
            }
            else
            {
                members = Enumerable.Empty<GetProductResult>();
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }
            return View(members);
        }

        public async Task<ActionResult> List()
        {
            IEnumerable<GetProductResult> members = null;

            var response = await _client.GetProductAsync();

            if (response.IsSuccessStatusCode)
            {

                members = await response.Content.ReadAsAsync<IList<GetProductResult>>();

            }
            else
            {
                members = Enumerable.Empty<GetProductResult>();
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }
            return View(members);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateProductCommand(){ Price = 1.000M, Weight = 1.00M, QuantityOnCreation = 1 });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateProductCommand request)
        {
            var result = await _client.PostProduct(request);
            var newViewModel = await result.Content.ReadAsAsync<BaseCommandResult>();

            if (newViewModel.Success)
            {
                TempData["SuccessMessage"] = newViewModel.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, newViewModel.Message);
                TempData["ErrorMessage"] = JsonConvert.DeserializeObject<IEnumerable<BaseCommandResult>>(newViewModel.Data.ToString());
            }

            return View(request);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Should inform ID");
            }

            UpdateProductCommand updateCommand = null;
            var response = await _client.GetProductById(id);

            if (response.IsSuccessStatusCode)
            {
                updateCommand = await response.Content.ReadAsAsync<UpdateProductCommand>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(updateCommand);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UpdateProductCommand request)
        {
            var result = await _client.PutProduct(request);
            var newViewModel = await result.Content.ReadAsAsync<BaseCommandResult>();

            if (newViewModel.Success)
            {
                TempData["SuccessMessage"] = newViewModel.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, newViewModel.Message);
                TempData["ErrorMessage"] = JsonConvert.DeserializeObject<IEnumerable<BaseCommandResult>>(newViewModel.Data.ToString());
            }

            return View(request);
        }
        #endregion

        #region Delete        
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Should inform Product ID");
            }

            var result = await _client.DeleteProduct(id);
            var newViewModel = await result.Content.ReadAsAsync<BaseCommandResult>();

            if (newViewModel.Success)
            {
                TempData["SuccessMessage"] = newViewModel.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, newViewModel.Message);
                TempData["ErrorMessage"] = JsonConvert.DeserializeObject<IEnumerable<BaseCommandResult>>(newViewModel.Data.ToString());
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Statistics
        [HttpGet]
        public async Task<ActionResult> Statistics()
        {
            GetStatisticsResult statisticsCommand = null;
            var response = await _client.GetStatistics();

            if (response.IsSuccessStatusCode)
            {
                statisticsCommand = await response.Content.ReadAsAsync<GetStatisticsResult>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }

            return View(statisticsCommand);
        }
        #endregion
    }
}
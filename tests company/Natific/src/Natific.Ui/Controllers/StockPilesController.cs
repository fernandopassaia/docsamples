using Natific.Ui.ApiInterface;
using Natific.Ui.Models;
using Natific.Ui.Models.Inputs;
using Natific.Ui.Models.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Natific.Ui.Controllers
{
    public class StockPilesController : Controller
    {
        #region Constructor and Index
        private readonly IStockPileApi _client;
        private readonly string _genericErrorMessage = "Server error, check if API is running.";

        public StockPilesController(IStockPileApi client)
        {
            _client = client;
        }
                
        public async Task<ActionResult> List(int id)
        {
            IEnumerable<GetStockPileResult> members = null;

            var response = await _client.GetStockPiles(id);

            if (response.IsSuccessStatusCode)
            {

                members = await response.Content.ReadAsAsync<IList<GetStockPileResult>>();

            }
            else
            {
                members = Enumerable.Empty<GetStockPileResult>();
                ModelState.AddModelError(string.Empty, _genericErrorMessage);
            }
            return View(members);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create(int id)
        {            
            return View(new CreateStockPileCommand() { ProductId = id, EntryWithDraw = 1, Quantity = 1, Description = "" });
            
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateStockPileCommand request)
        {
            
            var result = await _client.PostStockPile(request);
            var newViewModel = await result.Content.ReadAsAsync<BaseCommandResult>();

            if (newViewModel.Success)
            {
                TempData["SuccessMessage"] = newViewModel.Message;
                return RedirectToAction("Index", "Products");
            }
            else
            {
                ModelState.AddModelError(string.Empty, newViewModel.Message);
                TempData["ErrorMessage"] = JsonConvert.DeserializeObject<IEnumerable<BaseCommandResult>>(newViewModel.Data.ToString());
            }

            return View(request);
        }
        #endregion
    }
}
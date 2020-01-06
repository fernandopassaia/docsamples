using Natific.Domain.Command;
using Natific.Domain.Command.Handlers;
using Natific.Domain.Command.Inputs;
using Natific.Domain.Command.Results;
using Natific.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Natific.Api.Controllers
{
    public class StockPileController : ApiController
    {
        private readonly IStockPileRepository _repo;
        private readonly StockPileHandler _handler;
        public StockPileController(IStockPileRepository repo, StockPileHandler handler)
        {
            _repo = repo;
            _handler = handler;
        }

        [Route("api/stockpiles/{id}")]
        [AllowAnonymous]
        public async Task<IEnumerable<GetStockPileResult>> GetById(int id)
        {
            return await _repo.GetAsync(id);
        }

        [HttpPost]
        [Route("api/stockpiles")]
        [AllowAnonymous]
        public async Task<IBaseCommandResult> Post([FromBody]CreateStockPileCommand command)
        {
            var result = _handler.HandleSaveAsync(command);
            return await (result);
        }
    }
}
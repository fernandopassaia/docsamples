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
    public class ProductController : ApiController
    {
        //because a simple example, i'll not take care to Versioning of the API, JWT, Race, Elmah (log) or other stuffs...

        private readonly IProductRepository _repo;
        private readonly ProductHandler _handler;
        public ProductController(IProductRepository repo, ProductHandler handler)
        {
            _repo = repo;
            _handler = handler;
        }

        [Route("api/products")]
        [AllowAnonymous]
        public async Task<IEnumerable<GetProductResult>> Get()
        {
            return await _repo.GetAsync();
        }

        [Route("api/products/{id}")]
        [AllowAnonymous]
        public async Task<GetProductResult> GetById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        [Route("api/products/statistics")]
        [AllowAnonymous]
        public async Task<GetStatisticsResult> GetStatistics()
        {
            return await _handler.HandleStatisticsAsync();
        }

        [HttpPost]
        [Route("api/products")]
        [AllowAnonymous]
        public async Task<IBaseCommandResult> Post([FromBody]CreateProductCommand command)
        {
            var result = _handler.HandleSaveAsync(command);
            return await (result);
        }

        [HttpPut]
        [Route("api/products")]
        [AllowAnonymous]
        public async Task<IBaseCommandResult> Put([FromBody]UpdateProductCommand command)
        {
            var result = _handler.HandleUpdateAsync(command);
            return await (result);
        }

        [HttpDelete]
        [Route("api/products/{id}")]
        [AllowAnonymous]
        public async Task<IBaseCommandResult> Delete(int id)
        {
            var result = _handler.HandleDeleteAsync(id);
            return await (result);
        }
    }
}
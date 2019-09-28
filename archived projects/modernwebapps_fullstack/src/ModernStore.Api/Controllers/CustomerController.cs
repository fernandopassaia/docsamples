using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Domain.Commands.Handlers;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Infra.Transactions;
using System.Threading.Tasks;

namespace ModernStore.Api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IUow _uow;
        private readonly CustomerCommandHandler _handler;
        public CustomerController(IUow uow, CustomerCommandHandler handler)
            : base(uow) //send my uow to BaseController
        {
            _uow = uow;
            _handler = handler;
        }

        #region Before my Controller Base and Pattern of Return
        //public IActionResult Post([FromBody]RegisterCustomerCommand command)
        //{
        //    var result = _handler.Handle(command);
        //    if (_handler.Valid)
        //    {
        //        _uow.Commit();
        //        return Ok(result);
        //    }
        //    else
        //        return BadRequest(_handler.Notifications);
        //}
        #endregion

        [HttpPost]
        [Route("v1/customers")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]RegisterCustomerCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}
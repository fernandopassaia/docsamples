using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidator;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Domain.Commands.Handlers;
using ModernStore.Infra.Transactions;

namespace ModernStore.Api.Controllers
{
    public class BaseController : Controller
    {
        //this is to Padronize all my returns of API
        private readonly IUow _uow;
        public BaseController(IUow uow)
        {
            _uow = uow;            
        }

        public async Task<IActionResult> Response(object result, IEnumerable<Notification> notifications)
        {
            if(!notifications.Any())
            {
                try
                {
                    _uow.Commit();
                    return Ok(new
                    {
                        success = true,
                        data = result
                    });
                }
                catch
                {
                    //log the error with something (like Elmah)
                    return BadRequest(new
                    {
                        success = false,
                        errors = new[] { "A Internal-Server error occured." }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    errors = notifications
                });
            }
        }
    }
}
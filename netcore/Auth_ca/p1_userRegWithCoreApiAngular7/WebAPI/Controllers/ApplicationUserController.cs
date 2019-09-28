using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Commands;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost] //POST: api/ApplicationUser/Register
        [Route("Register")]
        public async Task<Object> PostApplicationUser(CreateUserCommand command)
        {
            //Note: This is just a sample. In a Real World DDD/SOLID please create a Service/Handle to do it, set correctly the Commanders,
            //yeah, create a "Pattern" to return a Pattern without this Try/Catch from Hell... Sorry about this No-Pattern Code...
            var applicationUser = new ApplicationUser()
            {
                UserName = command.UserName,
                Email = command.Email,
                FullName = command.FullName
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, command.Password);
                return Ok(result);
            }
            catch(Exception error)
            {
                throw error;
            }
        }
    }
}
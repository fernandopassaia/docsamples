using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        //Here I'll inject AppSettings into Application, because I'll use it in other places, Like ApplicationUserController to recovery the SecretKey
        //for Token Generation - So i have to Inject it in application. NOTE: ApplicationSettings is a MODEL that i've created on Models Folder
        private readonly ApplicationSettings _appSettings;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost] //POST: api/ApplicationUser/Register
        [Route("Register")]
        public async Task<Object> PostApplicationUser(CreateUserCommand command)
        {
            command.Role = "Customer";//here I will pass the Role this is not the best implementation, it's just for TEST
            //purpose and show how Roles works on WebApi IdentityServer and how to implement it on API consumed by Angular UI.

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
                await _userManager.AddToRoleAsync(applicationUser, command.Role); //register the role
                return Ok(result);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST: /api/ApplicationUser/Login
        public async Task<ActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password)) //InTimes: BadCode, I don't like it, but, following article
            {                
                var role = await _userManager.GetRolesAsync(user); //Here I'll get the ROLE of the user (admin, customer, whathever)
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    //Expires = DateTime.UtcNow.AddMinutes(5), //this token will expire after 5 minutes of generation
                    Expires = DateTime.UtcNow.AddDays(3), //this token will expire after 3 Days

                    //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@1234Fd@")), SecurityAlgorithms.HmacSha256Signature) //change to take my key from appsettings.json
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature) //change to take my key from appsettings.json
                };

                //now creation of the token using the variable under
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken); //now to write this token
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}
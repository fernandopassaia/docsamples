using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FutureOfMedia.Domain.Commands;
using FutureOfMedia.Domain.Commands.Inputs;
using FutureOfMedia.Domain.Commands.Results;
using FutureOfMedia.Domain.Handlers;
using FutureOfMedia.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FutureOfMedia.Api.Controllers
{
    public class UserController : Controller
    {
        //The way that API works is simple:
        //(1) I Need a Command (like CreateCustomerCommand)
        //(2) Then i will send this Command to Handler
        //(3) Then i just wait the result... that's it.
        //(4) To Get Data, i'll do it right on Repository
        //I'll Do a "Simply" Versioning, just with "v1" tag.

        private readonly IUserRepository _repository;
        private readonly UserHandler _handler;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UserController(IUserRepository repository, UserHandler handler, IHostingEnvironment hostingEnvironment)
        {
            _repository = repository;
            _handler = handler;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("v1/users")]
        public async Task<IEnumerable<GetUserResult>> Get()
        {
            return await _repository.GetAsync();
        }

        [HttpGet]
        [Route("v1/users/{id}")]
        public async Task<IBaseCommandResult> Get(int id)
        {            
            return await _repository.GetUserDetailAsync(id);
        }

        [HttpGet]
        [Route("v1/users/logged")]
        public async Task<IBaseCommandResult> GetLogged()
        {
            return await _repository.GetLoggedUserDetailAsync(FakeUserResolver.loggedUser);
        }

        [HttpPost]
        [Route("v1/users")]
        public async Task<IBaseCommandResult> Post([FromBody]CreateUserCommand command)
        {
            var result = await _handler.HandleSaveAsync(command);
            return result;
        }

        [HttpPut]
        [Route("v1/users")]
        public async Task<IBaseCommandResult> Put([FromBody]UpdateUserCommand command)
        {
            var result = await _handler.HandleUpdateAsync(command);
            return result;
        }

        [HttpDelete]
        [Route("v1/users/{id}")]
        public async Task<IBaseCommandResult> Delete(int id)
        {
            var result = await _handler.HandleDeleteAsync(id);
            return result;
        }

        [HttpPost]
        [Route("v1/users/image/{id}")]
        public async Task<IBaseCommandResult> PutImage(IFormFile image, int id)
        {
            var fileName = Path.GetFileName(image.FileName);
            var fileExtension = Path.GetExtension(image.FileName);
            var filePathToSave = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\User" + id + fileExtension;
            if ((fileExtension != ".jpg" && fileExtension != ".png") || (image == null || image.Length == 0))
                return new BaseCommandResult(false, "Please send a Valid Jpg/Png Image", null);

            //now i call my method that resizes the image (see doc inside Resolver)
            ImageResolver.ResizeAndSaveImage(image.OpenReadStream(), filePathToSave);
            //now i call the Handler to Save the FilePath to the User Profile
            var result = await _handler.HandleImageUploadAsync(id, filePathToSave);            
            return result;            
        }
    }
}
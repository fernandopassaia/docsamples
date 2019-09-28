using FluentValidator;
using FutureOfMedia.Domain.Commands;
using FutureOfMedia.Domain.Commands.Inputs;
using FutureOfMedia.Domain.Entities;
using FutureOfMedia.Domain.Repositories;
using FutureOfMedia.Domain.ValueObjects;
using System.Threading.Tasks;

namespace FutureOfMedia.Domain.Handlers
{
    public class UserHandler : Notifiable
    {
        private readonly IUserRepository _UserRepository;
        public UserHandler(IUserRepository repo)
        {
            _UserRepository = repo;
        }

        public async Task<BaseCommandResult> HandleSaveAsync(CreateUserCommand command)
        {
            var actualNumberOfUsers = await _UserRepository.GetNumberOfUsers();
            if(actualNumberOfUsers > 100)
                return new BaseCommandResult(false, "APP Reachs the Limit of 100 users. Cannot Add anymore.", null);

            var newName = new Name(command.FirstName, command.LastName);
            AddNotifications(newName.Notifications);

            var newUser = new User(newName, command.Email, command.Phone);
            AddNotifications(newUser.Notifications); //here i'll add the "errors" on my Entities, if any
            
            if (!Valid) //here i check the "valid" status (if we have erros, i will return to my API)
                return new BaseCommandResult(false, "Cannot Add User, fix errors:", Notifications);

            await _UserRepository.SaveAsync(newUser);
            return new BaseCommandResult(true, "User created with Success!", command);
        }

        public async Task<BaseCommandResult> HandleUpdateAsync(UpdateUserCommand command)
        {
            var actualUser = await _UserRepository.GetUserAsync(command.UserId);
            if (actualUser == null)
                return new BaseCommandResult(false, "Cannot Find User with this ID", null);

            var newName = new Name(command.FirstName, command.LastName);
            AddNotifications(newName.Notifications);

            actualUser.Update(newName, command.Email, command.Phone, command.EmailVisible, command.PhoneVisible);            
            AddNotifications(actualUser.Notifications);

            if (!Valid)
                return new BaseCommandResult(false, "Cannot Update User, fix errors:", Notifications);

            await _UserRepository.UpdateAsync(actualUser);

            //note: i could return "actualUser" without a DB operation, but the exercise tells to return own profile
            //endpoint, so mail/phone should be visible/hidden according config. I will method to load user again
            var updatedUser = await _UserRepository.GetUserDetailAsync(command.UserId);
            return new BaseCommandResult(true, "User updated with Success!", updatedUser);
        }

        public async Task<BaseCommandResult> HandleDeleteAsync(int id)
        {
            //note: product will not be removed, but market as removed
            var actualUser = await _UserRepository.GetUserAsync(id);
            if (actualUser == null)
                return new BaseCommandResult(false, "Cannot Find User with this ID", null);
                        
            await _UserRepository.RemoveAsync(actualUser);
            return new BaseCommandResult(true, "User Deleted with Success!", actualUser);
        }

        public async Task<BaseCommandResult> HandleImageUploadAsync(int id, string imagePath)
        {
            var actualUser = await _UserRepository.GetUserAsync(id);
            if (actualUser == null)
                return new BaseCommandResult(false, "Cannot Find User with this ID", null);
            actualUser.UpdateProfileImage(imagePath);

            await _UserRepository.UpdateAsync(actualUser);
                        
            var updatedUser = await _UserRepository.GetUserDetailAsync(id);
            return new BaseCommandResult(true, "User Image-Profile updated with Success!", updatedUser);
        }
    }
}

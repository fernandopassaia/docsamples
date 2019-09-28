using FutureOfMedia.Domain.Commands;
using FutureOfMedia.Domain.Commands.Results;
using FutureOfMedia.Domain.Entities;
using FutureOfMedia.Domain.Repositories;
using FutureOfMedia.Infra.Contexts;
using FutureOfMedia.Infra.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutureOfMedia.Infra.Repositories
{
    //NOTE: Take a Look at the Interface for Documentation
    public class UserRepository : IUserRepository
    {
        private readonly FutureOfMediaContext _context;

        public UserRepository(FutureOfMediaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetUserResult>> GetAsync()
        {
            var data = await _context.User.ToListAsync();

            return data.Select(usr => new GetUserResult
            {
                UserId = usr.UserId,
                FirstName = usr.Name.FirstName,
                LastName = usr.Name.LastName,
                ProfilePictureUrl = ImageUrlHelper.ReturnImgUrl(usr.UserId, usr.ProfilePictureUrl)
            });
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<int> GetNumberOfUsers()
        {
            var data = await _context.User.ToListAsync();
            return data.Count;
        }
        
        public async Task<IBaseCommandResult> GetLoggedUserDetailAsync(string name)
        {
            var data = await _context.User.Where(p => p.Name.FirstName == name).FirstAsync();
            if (data == null) return new BaseCommandResult(false, "Cannot Find User with Name " + name, null);

            return new BaseCommandResult(true, "User found. Here is data:", 
            new GetLoggedUserDetailResult
            {
                UserId = data.UserId,
                FirstName = data.Name.FirstName,
                LastName = data.Name.LastName,
                ProfilePictureUrl = ImageUrlHelper.ReturnImgUrl(data.UserId, data.ProfilePictureUrl),
                EmailAddress = data.EmailAddress,
                EmailVisible = data.EmailVisible,
                PhoneNumber = data.PhoneNumber,
                PhoneVisible = data.PhoneVisible
            });
        }

        public async Task<IBaseCommandResult> GetUserDetailAsync(int id)
        {
            var data = await _context.User.FindAsync(id);
            if (data == null) return new BaseCommandResult(false, "Cannot Find User with Id " + id, null);

            return new BaseCommandResult(true, "User found. Here is data:",
            new GetUserDetailResult
            {
                UserId = data.UserId,
                FirstName = data.Name.FirstName,
                LastName = data.Name.LastName,
                ProfilePictureUrl = ImageUrlHelper.ReturnImgUrl(data.UserId, data.ProfilePictureUrl),
                EmailAddress = data.getUserEmail(data),
                PhoneNumber = data.getUserPhone(data)
            });
        }
                
        public async Task RemoveAsync(User user)
        {            
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
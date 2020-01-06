using FutureOfMedia.Domain.Commands;
using FutureOfMedia.Domain.Commands.Inputs;
using FutureOfMedia.Domain.Commands.Results;
using FutureOfMedia.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutureOfMedia.Domain.Repositories
{
    public interface IUserRepository
    {
        //How it Works: On Post/Put/Delete - WebApi Controller will send a DTO to Handler. Handler will check (validate), if fails, will return in
        //"BaseCommandResult" a friendly message and the erros (in a array). If Pass, Handler will give a Friendly-message back to WebApi including
        //the updated Object in "data" field. The meaning of "BaseCommandResult" is always give back a Padronized-Answer to the UI. On the "Get"
        //methods i will return a "false" and a friendly "error" message, in case of not-found the search-criteria, or the result in "data" field.

        Task<IEnumerable<GetUserResult>> GetAsync();        
        Task<User> GetUserAsync(int id); 
        Task<int> GetNumberOfUsers();

        //In this Two Methods i use the Strategy to return a BaseCommandResult, so in case i don't find, i can send a "friendly" message.
        //I can simplify it sending back a "null" or "new GetUserDetailResult" empty, but in this case, i prefer this strategy to give good messages.
        Task<IBaseCommandResult> GetUserDetailAsync(int id);
        Task<IBaseCommandResult> GetLoggedUserDetailAsync(string emailLogin);

        //because i don't have any more table, i will not use/implement UnitOfWork (Transactions)
        Task SaveAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(User user);
    }
}

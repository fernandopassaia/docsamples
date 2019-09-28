using Natific.Domain.Command.Results;
using Natific.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Natific.Domain.Repositories
{
    public interface IStockPileRepository
    {
        //How it Works: On Post/Put/Delete - WebApi Controller will send a DTO to Handler. Handler has to check (validate) - if validation Fails,
        //Handler will send a error-message with Details back. If Pass, Handler will convert to a Database-Object (A Entity, like Product) and then
        //send it to Repo-Layer to Persist on DB. Then Handler will create a Padronized-Answer (CommandResult) and send back to WebApi to show on UI.
        //To GET Methods Data Will be Taken Right from Repo Layer, because we don't need validations or any aditional work, just send data back...
        //To Understand the Returns take a look to "BaseCommandResult".

        Task<IEnumerable<GetStockPileResult>> GetAsync(int id);
        Task SaveAsync(StockPile stockPile);
    }
}

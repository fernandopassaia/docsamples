using FluentValidator;
using Natific.Domain.Command.Inputs;
using Natific.Domain.Entities;
using Natific.Domain.Repositories;
using System.Threading.Tasks;

namespace Natific.Domain.Command.Handlers
{
    public class StockPileHandler : Notifiable
    {
        //Read DOC on IProductRepository
        private readonly IStockPileRepository _StockPileRepository;
        private readonly IProductRepository _ProductRepository;
        public StockPileHandler(IStockPileRepository stoRepo, IProductRepository proRepo)
        {
            _StockPileRepository = stoRepo;
            _ProductRepository = proRepo;
        }

        public async Task<BaseCommandResult> HandleSaveAsync(CreateStockPileCommand command)
        {
            var product = await _ProductRepository.GetDetailsByIdAsync(command.ProductId);

            var newStockPile = new StockPile(command.Description.ToString(), command.EntryWithDraw, command.Quantity, product);
            AddNotifications(newStockPile.Notifications);

            if (!Valid)
                return new BaseCommandResult(false, "Problem With Stock Management. Fix the errors, operation cancelled!", Notifications);

            //first of all i will do the Entry or WithDraw of Product
            if (command.EntryWithDraw == 1)
            product.IncreaseQuantity(command.Quantity);
            else
                product.DecreaseQuantity(command.Quantity);

            AddNotifications(product.Notifications);
            if (!Valid)
                return new BaseCommandResult(false, "Problem With Stock Management. Fix the errors, operation cancelled!", Notifications);

            await _ProductRepository.UpdateAsync(product); //if everything is OK i will save the product with new Stock/Quantity
            await _StockPileRepository.SaveAsync(newStockPile); //note: In a Real Scenario, i'll use UnityOfWork here...
            return new BaseCommandResult(true, "Stock Updated and Registered with Success!", command);
        }
    }
}

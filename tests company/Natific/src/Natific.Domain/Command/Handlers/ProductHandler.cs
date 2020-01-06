using FluentValidator;
using Natific.Domain.Command.Inputs;
using Natific.Domain.Command.Results;
using Natific.Domain.Entities;
using Natific.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Natific.Domain.Command.Handlers
{
    public class ProductHandler : Notifiable
    {
        //Read DOC on IProductRepository
        private readonly IProductRepository _ProductRepository;
        public ProductHandler(IProductRepository repo)
        {
            _ProductRepository = repo;
        }

        public async Task<BaseCommandResult> HandleSaveAsync(CreateProductCommand command)
        {
            var newProduct = new Product(command.Name.ToString(), command.Price, command.Description.ToString(), command.Weight, command.QuantityOnCreation);
            AddNotifications(newProduct.Notifications);

            if (!Valid)
                return new BaseCommandResult(false, "Cannot Add Product, fix errors:", Notifications);

            await _ProductRepository.SaveAsync(newProduct);
            return new BaseCommandResult(true, "Product created with Success!", command);
        }

        public async Task<BaseCommandResult> HandleUpdateAsync(UpdateProductCommand command)
        {
            var actualProduct = await _ProductRepository.GetDetailsByIdAsync(command.ProductId);
            if(actualProduct == null)
                return new BaseCommandResult(false, "Cannot Find Product with this ID", null);

            actualProduct.Update(command.Name.ToString(), command.Price, command.Description.ToString(), command.Weight);
            
            AddNotifications(actualProduct.Notifications);

            if (!Valid)
                return new BaseCommandResult(false, "Cannot Update Product, fix errors:", Notifications);

            await _ProductRepository.UpdateAsync(actualProduct);
            return new BaseCommandResult(true, "Product updated with Success!", command);
        }

        public async Task<BaseCommandResult> HandleDeleteAsync(int id)
        {
            //note: product will not be removed, but market as removed
            var actualProduct = await _ProductRepository.GetDetailsByIdAsync(id);
            if (actualProduct == null)
                return new BaseCommandResult(false, "Cannot Find Product with this ID", null);

            actualProduct.Deactivate();
            await _ProductRepository.RemoveAsync(actualProduct);
            return new BaseCommandResult(true, "Product Deleted with Success!", actualProduct);
        }

        public async Task<GetStatisticsResult> HandleStatisticsAsync()
        {            
            var products = await _ProductRepository.GetAsync();
            decimal TotalWeight = 0;
            decimal TotalPrice = 0;
            var MostItemStock = products.ToList().OrderByDescending(p => p.Quantity).First();
            var MostWeightStock = products.ToList().OrderByDescending(p => p.TotalWeight).First();

            products.ToList().ForEach(item => {
                TotalWeight += item.TotalWeight;
                TotalPrice += item.TotalPrice;
            });

            return new GetStatisticsResult() { TotalWeight = TotalWeight, TotalPrice = TotalPrice, MostItemStock = MostItemStock.Name, MostWeightStock = MostWeightStock.Name };
        }
    }
}
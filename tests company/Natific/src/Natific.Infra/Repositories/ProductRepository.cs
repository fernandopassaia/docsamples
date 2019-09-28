using Natific.Domain.Command.Results;
using Natific.Domain.Entities;
using Natific.Domain.Repositories;
using Natific.Infra.Contexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Natific.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //Read DOC on IProductRepository
        private readonly NatificContext _context;
        public ProductRepository(NatificContext context)
        {
            _context = context;
        }

        public async Task<GetProductResult> GetByIdAsync(int id)
        {
            var data = await _context.Product.FindAsync(id);

            return new GetProductResult 
            {
                ProductId = data.ProductId,
                Name = data.Name,
                Price = data.Price,
                Description = data.Description,
                Weight = data.Weight,
                Quantity = data.QuantityOnHand
            };
        }

        public async Task<Product> GetDetailsByIdAsync(int id)
        {
            //the difference to GetById is that here we have LOG (Entity)
            return await _context.Product.FindAsync(id);
        }

        public async Task<IEnumerable<GetProductResult>> GetAsync()
        {            
            var data =  await _context.Product.Where(p => p.Active == true).OrderBy(p => p.Name).ToListAsync();
            return data.Select(pr => new GetProductResult
            {
                ProductId = pr.ProductId,
                Name = pr.Name,
                Price = pr.Price,
                Description = pr.Description,
                Weight = pr.Weight,
                Quantity = pr.QuantityOnHand,
                TotalPrice = (pr.Price * pr.QuantityOnHand),
                TotalWeight = (pr.Weight * pr.QuantityOnHand)
            });
        }

        public async Task RemoveAsync(Product product)
        {            
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

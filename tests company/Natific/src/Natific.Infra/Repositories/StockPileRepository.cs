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
    public class StockPileRepository : IStockPileRepository
    {
        //Read DOC on IStockPileRepository
        private readonly NatificContext _context;
        public StockPileRepository(NatificContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetStockPileResult>> GetAsync(int id)
        {
            var data = await _context.StockPick.Where(p => p.ProductId == id).OrderByDescending(p => p.CreatedIn).ToListAsync();
            return data.Select(x => new GetStockPileResult
            {
                //because this is just a DTO, i format the data here...
                Description = x.Description,
                EntryWithDraw = x.EntryWithDraw.ToString()
                .Replace("1","Entry").Replace("2","WithDraw"),
                Quantity = x.Quantity,
                StockPileId = x.StockPileId,
                Date = x.CreatedIn.ToString("yyyy.MM.dd")
            });
        }

        public async Task SaveAsync(StockPile stockPile)
        {
            _context.StockPick.Add(stockPile);
            await _context.SaveChangesAsync();
        }
    }
}

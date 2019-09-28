using System;

namespace Natific.Domain.Command.Results
{
    public class GetStockPileResult
    {
        public int StockPileId { get; set; }
        public string Description { get; set; }
        public string EntryWithDraw { get; set; } //1 Entry 2 WithDraw
        public int Quantity { get; set; }
        public string Date { get; set; }
    }
}

namespace Natific.Domain.Command.Inputs
{
    public class CreateStockPileCommand
    {
        public string Description { get; set; }
        public int EntryWithDraw { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
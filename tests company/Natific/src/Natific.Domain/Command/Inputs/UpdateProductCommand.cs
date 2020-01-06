namespace Natific.Domain.Command.Inputs
{
    public class UpdateProductCommand
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        //cannot update quantity, just over Entry or WithDraw methods
        //public decimal Quantity { get; set; }
    }
}

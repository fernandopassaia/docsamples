using System.ComponentModel.DataAnnotations;

namespace Natific.Ui.Models.Inputs
{
    public class CreateStockPileCommand
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }
        [Display(Name = "Type Moviment")]
        public int EntryWithDraw { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
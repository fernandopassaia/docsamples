using System;
using System.ComponentModel.DataAnnotations;

namespace Natific.Ui.Models.Results
{
    public class GetStockPileResult
    {
        [Display(Name = "Id")]
        public int StockPileId { get; set; }
        public string Description { get; set; }

        [Display(Name = "Type")]
        public string EntryWithDraw { get; set; }
                
        public int Quantity { get; set; }

        [Display(Name = "Made in")]
        public string Date { get; set; }
    }
}
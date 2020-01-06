using System.ComponentModel.DataAnnotations;

namespace Natific.Ui.Models.Results
{
    public class GetProductResult
    {
        [Display(Name = "Id")]
        public int ProductId { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        public decimal Weight { get; set; }

        [Display(Name = "Stock")]
        public int Quantity { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Total Weight")]
        public decimal TotalWeight { get; set; }
    }
}
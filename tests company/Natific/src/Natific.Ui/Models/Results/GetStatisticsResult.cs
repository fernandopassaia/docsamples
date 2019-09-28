using System.ComponentModel.DataAnnotations;

namespace Natific.Ui.Models.Results
{
    public class GetStatisticsResult
    {
        //total weight, total price, most item in stock, most weight in stock
        [Display(Name = "Total Weight")]
        public decimal TotalWeight { get; set; }

        [Display(Name = "Total Price")]
        [DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Most Item in Stock")]
        public string MostItemStock { get; set; }

        [Display(Name = "Most Weight in Stock")]
        [DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        public string MostWeightStock { get; set; }
    }
}
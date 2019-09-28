using System.ComponentModel.DataAnnotations;

namespace Natific.Ui.Models.Inputs
{
    public class UpdateProductCommand
    {
        [Display(Name = "ID")]
        public int ProductId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        public decimal Weight { get; set; }
    }
}
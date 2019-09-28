using System.ComponentModel.DataAnnotations;

namespace Natific.Ui.Models.Inputs
{
    public class CreateProductCommand
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        //it will allow values like 500 - 1,000 - 10,000 - until 999,999
        [DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }
                
        [DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        public decimal Weight { get; set; }

        [Display(Name = "Quantity on Creation")]
        public int QuantityOnCreation { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Bim.WebUI.Models
{
    public class ManufacturerProductViewModel
    {
        public ManufacturerViewModel Manufacturer { get; set; } = new ManufacturerViewModel();

        public IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();
    }
}
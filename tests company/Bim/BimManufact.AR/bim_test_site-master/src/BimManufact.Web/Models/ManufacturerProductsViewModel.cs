using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BimManufact.Web.Models
{
    public class ManufacturerProductsViewModel
    {
        public ManufacturerViewModel Manufacturer { get; set; } = new ManufacturerViewModel();

        public IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();
    }
}
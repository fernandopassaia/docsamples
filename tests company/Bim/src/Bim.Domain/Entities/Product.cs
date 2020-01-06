using System.ComponentModel.DataAnnotations.Schema;

namespace Bim.Domain.Entities
{
    public class Product : Base
    {
        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }

        public int ManufacturerId { get; set; }
    }
}
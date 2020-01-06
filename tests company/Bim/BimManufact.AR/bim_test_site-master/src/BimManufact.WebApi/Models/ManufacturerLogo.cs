using System.ComponentModel.DataAnnotations.Schema;

namespace BimManufact.WebApi.Models
{
    public class ManufacturerLogo
    {
        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerLogoId { get; set; }

        public byte[] Content { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
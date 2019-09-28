using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bim.Domain.Entities
{
    public class ManufacturerImage
    {
        [Key]
        public int ImageId { get; set; }

        public byte[] Content { get; set; }

        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }

        public int ManufacturerId { get; set; }
    }
}

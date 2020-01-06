using System.ComponentModel.DataAnnotations.Schema;

namespace BimManufact.WebApi.Models
{
    public class ProductImage
    {
        [ForeignKey(nameof(Product))]
        public int ProductImageId { get; set; }

        public byte[] Content { get; set; }

        public virtual Product Product { get; set; }
    }
}
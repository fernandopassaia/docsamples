using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bim.Domain.Entities
{
    public class ProductImage
    {
        //I've created more than one table, because if someday needs,
        //will be easyly to have 1:N (more than one image per product)

        [Key]
        public int ImageId { get; set; }

        public byte[] Content { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int ProductId { get; set; }
    }
}

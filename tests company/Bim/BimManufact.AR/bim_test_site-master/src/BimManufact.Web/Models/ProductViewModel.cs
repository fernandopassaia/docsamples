using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BimManufact.Web.Models
{
    public class ProductViewModel
    {
        public int ManufacturerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int ProductId { get; set; }

        public HttpPostedFile Image { get; set; }
    }
}
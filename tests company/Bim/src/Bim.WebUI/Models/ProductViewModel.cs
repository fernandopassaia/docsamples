using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Bim.WebUI.Models
{
    public class ProductViewModel
    {
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int ManufacturerId { get; set; }

        public HttpPostedFile Image { get; set; }
    }
}
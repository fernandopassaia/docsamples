using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Bim.WebUI.Models
{
    public class ManufacturerViewModel
    {
        //i know i could add a reference to Domain and use it
        //but i prefer to keep UI independent from it
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int TotalNumberOfProducts { get; set; }

        public HttpPostedFile Image { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace BimManufact.WebApi.Models
{
    public abstract class ManufacturerBase
    {
        public int ManufacturerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
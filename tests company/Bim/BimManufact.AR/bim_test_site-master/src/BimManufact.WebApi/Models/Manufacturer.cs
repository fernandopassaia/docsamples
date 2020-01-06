using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BimManufact.WebApi.Models
{
    public class Manufacturer : ManufacturerBase
    {
        public ICollection<Product> Products { get; set; }

        public virtual ManufacturerLogo Logo { get; set; }

        [Required]
        public Guid AuditCreatedBy { get; set; }

        [Required]
        public DateTime AuditCreatedDate { get; set; }

        [Required]
        public Guid AuditLastModifiedBy { get; set; }

        [Required]
        public DateTime AuditLastModifiedDate { get; set; }
    }
}
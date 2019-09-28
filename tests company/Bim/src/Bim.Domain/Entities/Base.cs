using System;
using System.ComponentModel.DataAnnotations;

namespace Bim.Domain.Entities
{
    public abstract class Base
    {
        //because tables has same fields with same sizes/specifications
        //i've created a base-class
                
        [Key]
        public int id { get; set; }

        [StringLength(100)]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please insert Name")]        
        public string Name { get; set; }
                
        public Guid createBy { get; set; }
                
        public Guid updateBy { get; set; }
                
        public DateTime createIn { get; set; }
                
        public DateTime updateIn { get; set; }        
    }
}

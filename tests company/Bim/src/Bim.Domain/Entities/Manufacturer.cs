using System.Collections.Generic;

namespace Bim.Domain.Entities
{
    public class Manufacturer : Base
    {
        public ICollection<Product> Products { get; set; }
    }
}
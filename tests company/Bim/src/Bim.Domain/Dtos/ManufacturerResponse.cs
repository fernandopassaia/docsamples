using Bim.Domain.Entities;

namespace Bim.Domain.Dtos
{
    public class ManufacturerResponse : Manufacturer
    {
        //i've created a DTO (CQRS) to transfer data because the Count
        public virtual int TotalNumberOfProducts { get; set; }
    }
}

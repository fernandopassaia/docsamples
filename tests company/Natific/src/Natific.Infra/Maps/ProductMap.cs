using Natific.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Natific.Infra.Maps
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        //Map Class: I don't need to create it, but i like to Separete Config of Table on Config-Classes.
        //I know i can use Data-Annotation on Models, but i Prefer Fluent-Validation.
        public ProductMap()
        {
            ToTable("Products");
            HasKey(c => c.ProductId);
            Property(c => c.Name).HasMaxLength(60).IsRequired();
            Property(c => c.Description).HasMaxLength(120).IsOptional();
            Property(c => c.Price).IsRequired().HasPrecision(18,3);
            Property(c => c.Weight).IsRequired().HasPrecision(18, 3);
            Property(c => c.QuantityOnHand).IsRequired();
            Property(c => c.Active).IsRequired();            
            Property(c => c.CreatedIn).IsRequired();
            Property(c => c.UpdatedIn).IsRequired();
        }
    }
}
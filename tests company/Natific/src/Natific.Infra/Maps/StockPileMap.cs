using Natific.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Natific.Infra.Maps
{
    public class StockPileMap : EntityTypeConfiguration<StockPile>
    {
        //Map Class: I don't need to create it, but i like to Separete Config of Table on Config-Classes.
        //I know i can use Data-Annotation on Models, but i Prefer Fluent-Validation.
        public StockPileMap()
        {
            ToTable("StockPiles");
            HasKey(c => c.StockPileId);
            Property(c => c.Description).HasMaxLength(80).IsOptional();
            Property(c => c.EntryWithDraw).IsRequired();
            Property(c => c.Quantity).IsRequired();
            Property(c => c.Status).IsRequired();
            Property(c => c.CreatedIn).IsRequired();
            Property(c => c.UpdatedIn).IsRequired();            
        }
    }
}

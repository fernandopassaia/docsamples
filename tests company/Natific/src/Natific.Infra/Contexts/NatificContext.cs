using Natific.Domain.Entities;
using Natific.Infra.Maps;
using System.Data.Entity;

namespace Natific.Infra.Contexts
{
    public class NatificContext : DbContext
    {
        //How to RUN:
        //(1) Right click on Natific Solution and select "Restore Nuget Package".
        //(2) Right click on Solution and REBUILD Solution.
        //(3) Right click on Solution Natific.Api and "Set as StartUp Project".
        //(4) Now Open "Natific.Api > Web.Config" and check SQL Server Connection String.
        //(5) Open Package Manager Console, project "Infra\Natific.Infra" and run on console:
        //Update-DataBase -StartUpProjectName "Natific.Api" -ProjectName "Natific.Infra"

        public NatificContext() : base("name=NatificConnection") //Set connection string on Natific.Api > Web.config        
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<StockPile> StockPick { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new StockPileMap());

            modelBuilder.Entity<StockPile>().HasRequired(p => p.Product)
                .WithMany(p => p.StockPiles)
                .HasForeignKey(p => p.ProductId);
            base.OnModelCreating(modelBuilder);
        }
    }
}

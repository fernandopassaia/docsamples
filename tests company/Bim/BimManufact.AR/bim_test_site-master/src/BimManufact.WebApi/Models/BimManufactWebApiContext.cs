using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace BimManufact.WebApi.Models
{
    public interface IBimManufactWebApiContext : IDisposable
    {
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();

        DbSet<Manufacturer> Manufacturers { get; }
        DbSet<ManufacturerLogo> ManufacturerLogos { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
    }

    public class BimManufactWebApiContext : DbContext, IBimManufactWebApiContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BimManufactWebApiContext() : base("name=BimManufactWebApiContext")
        {
        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ManufacturerLogo> ManufacturerLogos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}

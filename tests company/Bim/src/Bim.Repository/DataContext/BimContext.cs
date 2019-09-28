using Bim.Domain.Dtos;
using Bim.Domain.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Bim.Repository.DataContext
{
    public interface IBimContext : IDisposable
    {
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();

        DbSet<Manufacturer> Manufacturers { get; set; }
        DbSet<ManufacturerImage> ManufacturerImages { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
    }

    public class BimContext : DbContext, IBimContext
    {
        //TO-DO: Externalize this connection to a separeted config file and share between projects
        //Set your string connection on Bim.Repository > App.config AND Bim.WebApi > Web.config
        public BimContext() : base("name=BimContext")
        {
            
        }
                
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ManufacturerImage> ManufacturerImages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //enabled to serializate to JSON
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            //these are just DTOs, so don't need to create it on DB
            modelBuilder.Ignore<ManufacturerRequest>();
            modelBuilder.Ignore<ManufacturerResponse>();
            modelBuilder.Ignore<ProductRequest>();
            modelBuilder.Ignore<ProductResponse>();
        }
    }
}
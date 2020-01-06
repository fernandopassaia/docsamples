using FutureOfMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FutureOfMedia.Infra.Contexts
{
    public class FutureOfMediaContext : DbContext
    {
        public FutureOfMediaContext(DbContextOptions<FutureOfMediaContext> options) : base(options) { }
        public FutureOfMediaContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(Settings.ConnectionString);
        }

        public DbSet<User> User { get; set; }

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ForSqlServerUseIdentityColumns();            
            ConfigUser(builder);
        }
        #endregion

        private void ConfigUser(ModelBuilder builder)
        {
            builder.Entity<User>(etd =>
            {
                etd.ToTable("User");
                etd.HasKey(c => c.UserId);
                etd.Property(c => c.EmailAddress).IsRequired().HasMaxLength(200);
                etd.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(200);
                etd.Property(c => c.ProfilePictureUrl).HasMaxLength(200);
                etd.Property(c => c.CreatedIn).IsRequired();
                etd.Property(c => c.UpdatedIn).IsRequired();
                //here i had to (1) show all my valueobject and (2) says to EF ignore the Notifications inside it                
                builder.Entity<User>().OwnsOne(c => c.Name).Ignore(p => p.Notifications);
                builder.Entity<User>().OwnsOne(c => c.Name).Property(c => c.FirstName).HasColumnName("FirstName").HasMaxLength(200);
                builder.Entity<User>().OwnsOne(c => c.Name).Property(c => c.LastName).HasColumnName("LastName").HasMaxLength(200);                
                etd.Ignore(c => c.Notifications);
            });
        }
    }
}

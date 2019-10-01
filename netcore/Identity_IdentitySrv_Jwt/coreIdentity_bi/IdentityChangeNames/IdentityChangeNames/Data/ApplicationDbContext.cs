using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityChangeNames.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityChangeNames.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid> //pra fazer funcionar alteração 3 e 4
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //altero o nome de uma tabela (nome da minha tabela de usuário)
            builder.Entity<ApplicationUser>().ToTable("User");

            builder.Entity<ApplicationRole>().ToTable("Role");

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");

            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole");

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");

            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");

            //altero o campo "Email" para "EmailAddress"
            builder.Entity<ApplicationUser>(b =>
            {
                b.Property(au => au.Email).HasColumnName("EmailAddress");
            });

            //3 alteração: Entrei na classe ApplicationUser e mudei meu IdentityUser pra Guid ao invés de string
            //4 alteração: Criei uma classe ApplicationRole pra também modificar pra Guid ao invés de string
            //Nota 1: Ao Rebuildar a aplicação, algumas classes que esperavam STRING começaram a reclamar do Guid. Só dar ".ToString()"
            //Nota 2: Precisei abrir a classe STARTUP e mudar o IdentityRole pra ApplicationRole nos Serviços...
            //NOTA 3: AspUserTokken, RoleClaim e UserClaim não permitem alterar a chave primária para Guid
        }
    }
}

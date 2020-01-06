using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OverViewIdentity.Data;
using OverViewIdentity.Models;
using OverViewIdentity.Services;

namespace OverViewIdentity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                
                //Nota Fernando: Adiciono opções para TRAVAR o usuário após X tentativas erradas por X minutos
                //Para que isso funcione no Controller Account > Login preciso mudar o lockoutOnFailure para TRUE

                //LockOut
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                //Pasword
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;

                //SignIn
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false; //twofactory auth

                //Tokens
                //options.Tokens.AuthenticatorTokenProvider
                //options.Tokens.ChangeEmailTokenProvider
                //options.Tokens.ChangePhoneNumberTokenProvider
                //options.Tokens.EmailConfirmationTokenProvider
                //options.Tokens.PasswordResetTokenProvider

                //User
                options.User.AllowedUserNameCharacters = "abcdefghijlmnopqrstuvxzwykABCDEFGHIJLMNOPQRSTUVXZWYK1234567890-._@+";
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Serviço de Cookies
            services.ConfigureExternalCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied"; //caminho Handler 403 Forbidden
                //options.ClaimsIssuer = ""; //obter/definir um Issue que será usado para criar qualquer claim 
                //options.Cookie.Domain = ""; //define o dominio do cookie - futuradata.com.br por exemplo, comentado por que está local
                //options.Cookie.Expiration = "";
                options.Cookie.HttpOnly = true; //será acessado pelo lado cliente também
                options.Cookie.Name = ".AspNetCore.Cookies"; //nome do cookie
                //options.Cookie.Path = "";
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax; //cookies que não devem ser anexados as solicitações cross-site
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;

                //options.CookieManager = ""; //classe que implemente o ICookieManager
                //options.DataProtectionProvider = ""; //classe que implementa o IDataProtectionProvider
                //options.EventsType
                options.ExpireTimeSpan = TimeSpan.FromDays(14); //dias que é válido
                options.LoginPath = "/Account/Login"; //caminho que o usuário será redirecionado se não tiver o cookie
                options.LogoutPath = "/Account/Logout";
                options.ReturnUrlParameter = "ReturnUrl"; //nome da variável que receberá a URL que o usuário deverá ser redirecionado após o login
                //options.SessionStore
                options.SlidingExpiration = true; //será o dia 
                //options.TicketDataFormat = 
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

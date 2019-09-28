using FutureOfMedia.Domain.Handlers;
using FutureOfMedia.Domain.Repositories;
using FutureOfMedia.Infra.Contexts;
using FutureOfMedia.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace FutureOfMedia.Api
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
            services.AddScoped<FutureOfMediaContext, FutureOfMediaContext>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<UserHandler, UserHandler>();            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //i had to ADD this Line because PostMan and AspNet Core are not in a mood...
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //i had to add this line, because MVC is converting my DTOs to Lowercase, so instead of "UserName" it's taking "userName" (even if class is UserName).
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddDbContext<FutureOfMediaContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //method to SWAGGER (OpenApi) works:
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "FutureOfMedia API", Version = "v1" });
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FutureOfMedia - V1");
            });


            //app.UseElmahIo();
        }
    }
}

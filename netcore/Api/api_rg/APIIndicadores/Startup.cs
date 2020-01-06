using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger; //importado para usar o Swagger

namespace APIIndicadores
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        //Fernando: 31/12/2018 14:14 (sim, ano novo, HU) e essa abaixo é a chamada pra criação do Swagger.
        //Swagger usará esses dados para gerar seu JSON de Documentação automática.
        services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Indicadores Econômicos",
                        Version = "v1",
                        Description = "Exemplo de API REST criada com o ASP.NET Core 2.2 para consulta a indicadores econômicos",
                        Contact = new Contact
                        {
                            Name = "Fernando Passaia",
                            Url = "https://github.com/fernandopassaia"
                        }
                    });
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

            // Ativando middlewares para uso do Swagger
            app.UseSwagger(); //swaggerUI é o que monta a interface gráfica (ele pega o JSON abaixo de doc e monta o site)
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Indicadores Econômicos V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

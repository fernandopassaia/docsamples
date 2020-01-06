using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MvcClient
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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            //services.AddAuthentication("NomeDoMeuCookie"); //falando de forma grosseira, aqui é o nome do meu Cookie
            //abaixo configurei ele usando Options pra poder enxergar melhor minhas opções
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";                
                
            }).AddCookie("Cookies")
            //.AddFacebook - aqui vou adicionando meus serviços, como facebook por exemplo...
            //agora como eu vou usar o OpenId, eu preciso configurar ele
            .AddOpenIdConnect("oidc", options => //oidc precisa ser IGUAL do que configurei acima
            {
                //Note que meu nome acima (oidc) precisa ser igual do cara acima, meu nome Cookies também...
                //Em Resumo: Após me autenticar no Identity Server, ele irá gravar no COOKIES a informação...                
                options.SignInScheme = "Cookies"; //após me logar, será em Cookies que o browser salvará a informação de usuário
                options.RequireHttpsMetadata = false; //permito meu APP rodar sem HTTPS
                options.Authority = "http://localhost:5000"; //meu servidor de identidade
                options.ClientId = "mvc.implicit"; //importante: esse é o nome do meu app, ele PRECISA estar no identity server

                //Token é pra me autenticar, id_token é pra acessar uma API. DICA QUE DÁ PAU PRA CARAMBA: Pra receber um Access Token,
                //eu preciso informar "token" no meu client... No Identity Server eu preciso informar a TAG "AllowAccessTokensViaBrowser = true",
                //dizendo que o client pode descer o Token que não tem problema... se faltar isso vai dar pau pra caralho!
                options.ResponseType = "token id_token"; //aqui eu digo qual é o tipo de token que quero receber...

                //isso funciona como aquela CHAVE que você gera no Facebook ou Google - meu Identity Server irá esperar essa chave
                options.ClientSecret = "@1234Fd@";
                options.SaveTokens = true; //pra salvar o Cookie no Token.

                //agora eu preciso informar o que eu QUERO que o meu servidor de identidade me retorne...
                options.Scope.Add("openid"); //aqui eu digo que eu PRECISO acessar o escopo de Nota Fiscal... estou requisitando ele!                
                options.Scope.Add("profile"); //aqui eu digo que eu PRECISO acessar o escopo de Nota Fiscal... estou requisitando ele!                
                options.Scope.Add("notafiscal"); //aqui eu digo que eu PRECISO acessar o escopo de Nota Fiscal... estou requisitando ele!
                //nota: no meu Identity Server, eu preciso falar que o escopo "mvc.implicit" pode acessar o escopo NotaFiscal...
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); //habilito autenticação para meu aplicativo - vem antes do StaticFiles
            app.UseStaticFiles();
            app.UseCookiePolicy();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

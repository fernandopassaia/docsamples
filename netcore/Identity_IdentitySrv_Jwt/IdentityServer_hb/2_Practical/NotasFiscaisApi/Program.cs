using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NotasFiscaisApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://+:5002") //boto minha API pra rodar na porta 5002...
                //5000 Porta do meu IdentityServer - 5001 Porta do meu MvcClient - 5002 Porta da minha API - 5003 Porta do meu MvcIdentityClient
                .UseStartup<Startup>();
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MvcClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://+:5001") //mudei minha porta pra não dar conflito com o IdentityServer (rodando em outro projeto)
                //5000 Porta do meu IdentityServer - 5001 Porta do meu MvcClient - 5002 Porta da minha API - 5003 Porta do meu MvcIdentityClient
                .UseStartup<Startup>();
    }
}

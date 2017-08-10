using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EComm.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        // Good Reads
        //      https://wildermuth.com/2017/07/06/Program-cs-in-ASP-NET-Core-2-0
        //      https://github.com/aspnet/MetaPackages/blob/dev/src/Microsoft.AspNetCore/WebHost.cs#L152
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration(AddToConfiguration)
                .UseStartup<Startup>()
                .Build();

        static void AddToConfiguration(WebHostBuilderContext whbc, IConfigurationBuilder configBuilder)
        {
            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("config.json", false, true);
        }
    }
}

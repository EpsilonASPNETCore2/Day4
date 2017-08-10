using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using EComm.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace EComm.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddXmlDataContractSerializerFormatters()
                .AddMvcOptions(opts => {
                    opts.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
                 });

            services.AddDbContext<DataContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("ECommConnection")));

            // Good Read: https://offering.solutions/blog/articles/2017/02/07/difference-between-addmvc-addmvcore/
            //services.AddMvcCore().AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // Issue: Default Route does not make sense for a pure Web API App
            //          {controller=Home}/{action=Index}/{id?}
            //app.UseMvcWithDefaultRoute();    

            DisplayConfiguration();
        }

        private void DisplayConfiguration()
        {
            Console.WriteLine("**********************************************");
            Console.WriteLine("Configuration Example");
            Console.WriteLine("**********************************************");

            Console.WriteLine("********* appsettings.json *******************");
            Console.WriteLine("ConnectionStrings:DefaultConnection " + 
                $"{Configuration["ConnectionStrings:DefaultConnection"]}");

            //Console.WriteLine("********* config.json ************************");
            //Console.WriteLine($"option1 = {Configuration["option1"]}");
            //Console.WriteLine($"option2 = {Configuration["option2"]}");
            //Console.WriteLine($"suboption1 = {Configuration["subsection:suboption1"]}");
            //Console.WriteLine();

            //Console.WriteLine("collection:");
            //Console.Write($"{Configuration["collection:0:Name"]}, ");
            //Console.WriteLine($"age {Configuration["collection:0:Age"]}");
            //Console.Write($"{Configuration["collection:1:Name"]}, ");
            //Console.WriteLine($"age {Configuration["collection:1:Age"]}");
            //Console.WriteLine();
        }
    }
}
